using System.Linq;
using System.Reflection;
using AutoMapper;
using iBlogs.Site.Core.Common.AutoMapper;
using iBlogs.Site.Core.Common.Caching;
using iBlogs.Site.Core.Common.CodeDi;
using iBlogs.Site.Core.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SLog=Serilog.Log;

namespace iBlogs.Site.Core.Common.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddIBlogs(this IServiceCollection services, IConfiguration configuration)
        {
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
                options.AssemblyNames = new[] { "*iBlogs.Site*" };
                //options.IgnoreAssemblies = new[] { "*Z.Dapper.Plus*", "*Dapper*", "*Hangfire*", "*Microsoft*", "ef*", "*AutoMapper*", "*Markdig*", "*Newtonsoft*", "*SixLabors*", "*System*" };
                options.IgnoreInterface = new[] { "*IEntityBase*", "*Caching*" };
            });
            services.AddAutoMapper(cfg =>
            {
                cfg.ValidateInlineMaps = false;
                cfg.AddProfile<AutoMapperProfile>();
            },
                Assembly.GetAssembly(typeof(ServiceCollection)));
            return services;
        }
    }
}
