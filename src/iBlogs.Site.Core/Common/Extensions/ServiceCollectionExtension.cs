using System.Reflection;
using AutoMapper;
using iBlogs.Site.Core.Common.AutoMapper;
using iBlogs.Site.Core.Common.CodeDi;
using iBlogs.Site.Core.EntityFrameworkCore;
using iBlogs.Site.Core.Option.Service;
using Microsoft.Extensions.DependencyInjection;

namespace iBlogs.Site.Core.Common.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddIBlogs(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddCoreDi(options =>
            {
                options.AssemblyNames = new[] {"*iBlogs.Site*"};
                //options.IgnoreAssemblies = new[] { "*Z.Dapper.Plus*", "*Dapper*", "*Hangfire*", "*Microsoft*", "ef*", "*AutoMapper*", "*Markdig*", "*Newtonsoft*", "*SixLabors*", "*System*" };
                options.IgnoreInterface = new[] { "*IEntityBase*" };
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
