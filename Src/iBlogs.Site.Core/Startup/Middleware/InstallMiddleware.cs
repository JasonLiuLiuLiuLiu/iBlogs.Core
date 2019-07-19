using System.Threading.Tasks;
using iBlogs.Site.Core.Common.Extensions;
using iBlogs.Site.Core.Install.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace iBlogs.Site.Core.Startup.Middleware
{
    public class InstallMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;
        private IInstallService _installService;
        private HttpContext _httpContext;

        public InstallMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task Invoke(HttpContext context, IInstallService installService)
        {
            _httpContext = context;
            _installService = installService;
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