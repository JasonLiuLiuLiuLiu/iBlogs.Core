using iBlogs.Site.Core.Common.Response;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace iBlogs.Site.Web.Filter
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ILogger<ExceptionFilter> _logger;

        public ExceptionFilter(IHostingEnvironment hostingEnvironment, ILogger<ExceptionFilter> logger)
        {
            _hostingEnvironment = hostingEnvironment;
            _logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            if (!_hostingEnvironment.IsDevelopment())
            {
                return;
            }

            if (context.HttpContext.Request.GetDisplayUrl().Contains("api"))
            {
                context.Result = new ObjectResult(ApiResponse.Fail(context.Exception.Message));
                _logger.LogError(context.Exception, context.Exception.Message);
                return;
            }

            context.Result = new StatusCodeResult(500);
        }
    }
}
