using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace iBlogs.Site.Web.Attribute
{
    public class ViewLayoutAttribute : ResultFilterAttribute
    {
        private string layout;
        public ViewLayoutAttribute(string layout)
        {
            this.layout = layout;
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.Result is ViewResult viewResult)
            {
                viewResult.ViewData["Layout"] = layout;
            }
        }
    }
}
