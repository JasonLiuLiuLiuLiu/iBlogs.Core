using System;
using System.IO;
using iBlogs.Site.Core.Common.Extensions;
using iBlogs.Site.Core.Git;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;

namespace iBlogs.Site.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var argsConfig = args.SetConfigInfo();

            var logConfig = new LoggerConfiguration()
#if DEBUG
                .MinimumLevel.Debug()
#else
                .MinimumLevel.Information()
#endif
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .Enrich.FromLogContext();

            if (argsConfig.ContainsKey("logToConsole") && argsConfig["logToConsole"].ToBool())
            {
                logConfig = logConfig.WriteTo.Console();
            }
            else
            {
                logConfig = logConfig.WriteTo.File(Path.Combine("log", "log.txt"), rollingInterval: RollingInterval.Hour);
            }

            logConfig.Enrich.With<ErrorLogNoticeEnricher>();

            Log.Logger = logConfig.CreateLogger();

            try
            {
                Log.Information("Starting web host");
                CreateWebHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Host terminated unexpectedly :{ex}");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", false, true)
                        .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true)
                        .AddEnvironmentVariables()
                        .AddCommandLine(args);
                })
                .UseStartup<Startup>()
                .UseSerilog();
    }
}