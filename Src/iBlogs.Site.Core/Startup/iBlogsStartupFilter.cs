using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using iBlogs.Site.Core.Common;
using iBlogs.Site.Core.Common.Extensions;
using iBlogs.Site.Core.Option.Service;
using iBlogs.Site.Core.Startup.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace iBlogs.Site.Core.Startup
{
    public class iBlogsStartupFilter : IStartupFilter
    {
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return app =>
            {
                var configuration = ServiceFactory.GetService<IConfiguration>();
                var option = ServiceFactory.GetService<IOptionService>();
                var appLifetime = ServiceFactory.GetService<IApplicationLifetime>();

                if (configuration["DbInstalled"].ToBool())
                    option.Load();

                appLifetime.ApplicationStarted.Register(() => { Console.WriteLine("iBlogs started."); });

                appLifetime.ApplicationStopping.Register(() =>
                {
                    Console.WriteLine(
                        "iBlogs is stopping,If you run this application at docker, please add \"--restart = always\" to the run command...");
                });

                appLifetime.ApplicationStopped.Register(() => { Console.WriteLine("iBlogs stopped."); });

                app.UseMiddleware<JwtInHeaderMiddleware>();

                app.UseMiddleware<InstallMiddleware>();

                next(app);
            };
        }
    }
}
