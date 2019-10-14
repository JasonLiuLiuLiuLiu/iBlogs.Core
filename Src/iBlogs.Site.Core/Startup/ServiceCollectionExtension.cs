using System.Reflection;
using System.Text;
using AutoMapper;
using iBlogs.Site.Core.Common;
using iBlogs.Site.Core.Common.AutoMapper;
using iBlogs.Site.Core.Common.Caching;
using iBlogs.Site.Core.Common.CodeDi;
using iBlogs.Site.Core.Storage;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
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


            SLog.Information("use memory cache");
            services.AddSingleton<ICacheManager, MemoryCacheManager>();

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddCoreDi(options =>
            {
                options.AssemblyNames = new[] { "*iBlogs.Site.Core*" };
                options.IgnoreInterface = new[] { "*IEntityBase*", "*Caching*" };
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
