using iBlogs.Site.Core.Option;
using iBlogs.Site.Core.Option.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace iBlogs.Site.Web.Attribute
{
    public class ViewTitleAttribute : ResultFilterAttribute
    {
        private readonly string _title;

        public ViewTitleAttribute(string title)
        {
            _title = title;
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            var optionService = (IOptionService)context.HttpContext.RequestServices.GetService(typeof(IOptionService));

            if (context.Result is ViewResult viewResult)
            {
                viewResult.ViewData["title"] = _title + optionService.Get(OptionKeys.SiteTitle, "iBlogs");
            }
        }
    }
}