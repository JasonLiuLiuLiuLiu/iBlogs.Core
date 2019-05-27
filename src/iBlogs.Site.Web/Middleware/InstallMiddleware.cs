using System;
using System.IO;
using System.Threading.Tasks;
using iBlogs.Site.Core;
using iBlogs.Site.Core.Common;
using iBlogs.Site.Core.Common.Extensions;
using iBlogs.Site.Core.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;

namespace iBlogs.Site.Web.Middleware
{
    public class InstallMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;
        private iBlogsContext _blogsContext;

        public InstallMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task Invoke(HttpContext context, iBlogsContext blogsContext)
        {
            _blogsContext = blogsContext;
            if (!Installed())
                context.Request.Path = "/install";
            await _next.Invoke(context);
        }

        private bool Installed()
        {
            return _configuration[ConfigKey.DbInstalled].ToBool();
        }
    }
}
