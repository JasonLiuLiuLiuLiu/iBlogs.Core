using System;
using iBlogs.Site.Core.Common.Extensions;
using iBlogs.Site.Core.EntityFrameworkCore;
using iBlogs.Site.Web.Converter;
using iBlogs.Site.Web.Filter;
using iBlogs.Site.Web.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Text;
using iBlogs.Site.Core.Option.Service;

namespace iBlogs.Site.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<iBlogsContext>(options =>
                {
                    options.UseMySql(Configuration.GetConnectionString("iBlogs"));
                });
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    var issuer = Configuration["Auth:JwtIssuer"];
                    var key = Configuration["Auth:JwtKey"];
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
            services.AddIBlogs();
            services.AddMvc(option =>
            {
                option.Filters.Add<LoginFilter>();
            }).AddJsonOptions(options =>
            {
                options.SerializerSettings.ContractResolver = new BlogsContractResolver();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IOptionService option)
        {
            if (Configuration["DbInstalled"].ToBool())
                option.Load();

            app.UseMiddleware<JwtInHeaderMiddleware>();

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

            app.UseMiddleware<InstallMiddleware>();

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