﻿using iBlogs.Site.Core.Common.Extensions;
using iBlogs.Site.Core.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using iBlogs.Site.Core.Install.Service;

namespace iBlogs.Site.Web.Middleware
{
    public class InstallMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;
        private readonly IInstallService _installService;
        private HttpContext _httpContext;

        public InstallMiddleware(RequestDelegate next, IConfiguration configuration, IInstallService installService)
        {
            _next = next;
            _configuration = configuration;
            _installService = installService;
        }

        public async Task Invoke(HttpContext context)
        {
            _httpContext = context;
            if (!await Installed())
                context.Request.Path = "/install";
            await _next.Invoke(context);
        }

        private async Task<bool> Installed()
        {
            if (_configuration["DbInstalled"].ToBool())
                return true;
            if (_httpContext.Request.Path.ToString().ToLower().Contains("install"))
                return true;
            return await _installService.InitializeDb();
        }
    }
}