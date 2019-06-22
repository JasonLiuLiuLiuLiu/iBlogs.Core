using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace iBlogs.Site.Web.Attribute
{
    public class ViewLayoutAttribute : ResultFilterAttribute
    {
        private readonly string _layout;

        public ViewLayoutAttribute(string layout)
        {
            _layout = layout;
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.Result is ViewResult viewResult)
            {
                viewResult.ViewData["Layout"] = _layout;
            }
        }
    }
}