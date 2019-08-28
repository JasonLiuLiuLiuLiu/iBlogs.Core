using System.Collections.Generic;
using System.Reflection;
using System.Text;
using AutoMapper;
using DotNetCore.CAP.Dashboard;
using iBlogs.Site.Core.Common;
using iBlogs.Site.Core.Common.AutoMapper;
using iBlogs.Site.Core.Common.Caching;
using iBlogs.Site.Core.Common.CodeDi;
using iBlogs.Site.Core.Common.Extensions;
using iBlogs.Site.Core.EntityFrameworkCore;
using iBlogs.Site.Core.Startup.Middleware;
using iBlogs.Site.MetaWeblog.Wrappers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SLog = Serilog.Log;

namespace iBlogs.Site.Core.Startup
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddIBlogs(this IServiceCollection services)
        {
            ServiceFactory.Services = services;
            var configuration = ServiceFactory.GetService<IConfiguration>();

            services.AddDbContextPool<iBlogsContext>(options =>
            {
                options.UseMySql(configuration.GetConnectionString("iBlogs"));
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    var issuer = configuration["Auth:JwtIssuer"];
                    var key = configuration["Auth:JwtKey"];
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidIssuer = issuer,
                        ValidAudience = issuer,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                        ValidateIssuer = true,
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,
                    };
                });

            var redisConnectionOption = new RedisConnectionOption()
            {
                ConnectionString = configuration["RedisConnectionString"]
            };
            var redisConnectionWrapper = new RedisConnectionWrapper(redisConnectionOption);
            if (redisConnectionWrapper.CheckConnection().Result)
            {
                SLog.Information("Use redis cache");
                services.AddSingleton<IRedisConnectionWrapper>(redisConnectionWrapper);
                services.AddScoped<ICacheManager, RedisCacheManager>();
            }
            else
            {
                SLog.Information("use memory cache");
                services.AddMemoryCache();
                services.AddSingleton<ICacheManager, MemoryCacheManager>();
            }

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddCoreDi(options =>
            {
                options.AssemblyNames = new[] { "*iBlogs.Site.Core*" };
                options.IgnoreInterface = new[] { "*IEntityBase*", "*Caching*" };
            });

            if (configuration["DbInstalled"].ToBool())
                services.AddCap(x =>
                {
                    x.UseEntityFramework<iBlogsContext>();
                    x.UseRabbitMQ(option =>
                    {
                        option.HostName = configuration["RabbitMqHost"] ?? "localhost";
                        option.Password = configuration["RabbitMqPWD"] ?? "guest";
                        option.UserName = configuration["RabbitMqUID"] ?? "guest";
                        option.Port = 5672;
                    });
                    x.UseDashboard(option =>
                    {
                        option.Authorization = new[]{new CapDashboardAuthorizationFilter()};
                    });
                });

            services.AddAutoMapper(cfg =>
            {
                cfg.ValidateInlineMaps = false;
                cfg.AddProfile<AutoMapperProfile>();
            },
                Assembly.GetAssembly(typeof(ServiceCollection)));

            services.AddTransient<IStartupFilter, iBlogsStartupFilter>();

            return services;
        }
    }
}
