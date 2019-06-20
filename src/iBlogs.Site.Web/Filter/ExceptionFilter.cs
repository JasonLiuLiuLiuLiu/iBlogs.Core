using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iBlogs.Site.Core.Common.Response;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace iBlogs.Site.Web.Filter
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IModelMetadataProvider _modelMetadataProvider;

        public ExceptionFilter(
            IHostingEnvironment hostingEnvironment,
            IModelMetadataProvider modelMetadataProvider)
        {
            _hostingEnvironment = hostingEnvironment;
            _modelMetadataProvider = modelMetadataProvider;
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

            var result = new ViewResult
            {
                StatusCode = 500,
                ViewName = "CustomError",
                ViewData = new ViewDataDictionary(_modelMetadataProvider, context.ModelState) { { "Exception", context.Exception } }
            };
            context.Result = result;
        }
    }
}
