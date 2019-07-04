using System;
using Microsoft.AspNetCore.Mvc;

namespace iBlogs.Site.Core.Common
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class AdminApiHttpPostAttribute: HttpPostAttribute
    {
        public AdminApiHttpPostAttribute(string template) : base("/admin/api/" + template)
        {
           
        }
    }
}
