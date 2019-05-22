using System;
using System.IO;
using System.Threading.Tasks;
using iBlogs.Site.Core;
using iBlogs.Site.Core.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace iBlogs.Site.Web.Middleware
{
    public class InstallMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public InstallMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task Invoke(HttpContext context)
        {
            if (!Installed())
                context.Request.Path = "/install";
            await _next.Invoke(context);
        }

        private bool Installed()
        {
            if (_configuration[ConfigKey.DbInstalled].ToBool())
                return true;
            if (File.Exists(Environment.CurrentDirectory +"\\"+
                            _configuration[ConfigKey.SqLiteDbFileName].IfNullReturnDefaultValue("iBlogs.db")))
                return true;
            return false;
        }
    }
}
