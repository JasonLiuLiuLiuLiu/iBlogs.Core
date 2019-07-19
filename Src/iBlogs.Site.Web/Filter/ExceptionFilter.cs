using iBlogs.Site.Core.Common.Response;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace iBlogs.Site.Web.Filter
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public ExceptionFilter(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
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
                return;
            }

            context.Result = new StatusCodeResult(500);
        }
    }
}
