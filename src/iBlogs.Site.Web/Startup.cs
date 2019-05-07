using System;
using System.IO;
using iBlogs.Site.Application;
using iBlogs.Site.Application.Extensions;
using iBlogs.Site.Application.Utils;
using iBlogs.Site.Application.Utils.CodeDi;
using iBlogs.Site.Web.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace iBlogs.Site.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            CheckDbInstallStatus();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCoreDi();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMiddleware<InstallMiddleware>();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Index}/{action=Index}/{id?}");
            });
        }

        private void CheckDbInstallStatus()
        {
            if (File.Exists(Environment.CurrentDirectory + "\\" + Configuration[ConfigKey.SqLiteDbFileName].IfNullReturnDefaultValue("iBlogs.db")))
                ConfigDataHelper.UpdateDbInstallStatus(true);
            else
                ConfigDataHelper.UpdateDbInstallStatus(false);
        }
    }
}
