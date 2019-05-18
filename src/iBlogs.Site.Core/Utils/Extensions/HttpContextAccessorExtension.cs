using iBlogs.Site.Core.Dto;
using Microsoft.AspNetCore.Http;

namespace iBlogs.Site.Core.Utils.Extensions
{
    public static class HttpContextAccessorExtension
    {
        public static CurrentUser CurrentUser(
            this HttpContext httpContext
        )
        {
            var httpUser = httpContext.User;
            return new CurrentUser();
        }
    }
}
