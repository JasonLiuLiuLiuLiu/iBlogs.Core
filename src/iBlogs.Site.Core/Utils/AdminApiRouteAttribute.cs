using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace iBlogs.Site.Core.Utils
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class AdminApiRouteAttribute: RouteAttribute
    {
        public AdminApiRouteAttribute(string template) : base("/admin/api/" + template)
        {
           
        }
    }
}
