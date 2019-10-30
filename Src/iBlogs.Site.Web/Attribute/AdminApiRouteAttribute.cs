using System;
using Microsoft.AspNetCore.Mvc;

namespace iBlogs.Site.Web.Attribute
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class AdminApiRouteAttribute : RouteAttribute
    {
        public AdminApiRouteAttribute(string template) : base("/admin/api/" + template)
        {

        }
    }
}
