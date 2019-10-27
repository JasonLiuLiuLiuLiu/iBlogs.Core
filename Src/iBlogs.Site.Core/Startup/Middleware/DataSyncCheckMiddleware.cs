using System;
using System.Threading.Tasks;
using iBlogs.Site.Core.Common.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace iBlogs.Site.Core.Startup.Middleware
{
    public class DataSyncCheckMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public DataSyncCheckMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task Invoke(HttpContext context)
        {
            if (!_configuration["DataIsSynced"].ToBool()&&!context.Request.Path.Value.Contains("/error/loading",StringComparison.OrdinalIgnoreCase))
            {
                context.Response.Redirect("/error/loading");
                return;
            }

            if (context.Request.Path.Value.Contains("/error/loading", StringComparison.OrdinalIgnoreCase) &&
                _configuration["DataIsSynced"].ToBool())
            {
                context.Response.Redirect("/index/index/1");
                return;
            }

            await _next.Invoke(context).ConfigureAwait(false);
        }
    }
}