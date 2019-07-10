using System;
using iBlogs.Site.Web.Converter;
using iBlogs.Site.Web.Filter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using iBlogs.Site.Core.Option.Service;
using iBlogs.Site.Core.Startup;

namespace iBlogs.Site.Web
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIBlogs();

            services.AddMvc(option =>
            {
                option.Filters.Add<LoginFilter>();
                option.Filters.Add<ExceptionFilter>();
            }).AddJsonOptions(options =>
            {
                options.SerializerSettings.ContractResolver = new BlogsContractResolver();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IOptionService option, IApplicationLifetime appLifetime, IServiceProvider serviceProvider)
        {
            app.UseStatusCodePages(async context =>
            {
                var response = context.HttpContext.Response;
                if (response.StatusCode == (int)HttpStatusCode.Unauthorized)
                {
                    response.Redirect("/admin/login");
                }
                else if (!env.IsDevelopment())
                {
                    if (response.StatusCode == (int)HttpStatusCode.NotFound)
                    {
                        response.Redirect("/error/error404");
                    }
                    else if (response.StatusCode == (int)HttpStatusCode.InternalServerError)
                    {
                        response.Redirect("/error/error500");
                    }
                    else
                    {
                        response.Redirect("/Error/Index/" + response.StatusCode);
                    }
                }
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "Admin",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Index}/{action=Index}/{id?}");
            });
        }
    }
}