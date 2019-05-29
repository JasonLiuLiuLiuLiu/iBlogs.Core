using System.Linq;
using System.Threading.Tasks;
using iBlogs.Site.Core.Common;
using iBlogs.Site.Core.User.DTO;
using iBlogs.Site.Core.User.Service;
using Microsoft.AspNetCore.Http;

namespace iBlogs.Site.Web.Middleware
{
    public class GetUserInfoMiddleware
    {
        private readonly IUserService _userService;
        private readonly RequestDelegate _next;

        public GetUserInfoMiddleware(IUserService userService, RequestDelegate next)
        {
            _userService = userService;
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.User.Claims.Any())
            {
                var uid = int.Parse(context.User.FindFirst(ClaimTypes.Uid)?.Value);
                var name = context.User.FindFirst(ClaimTypes.Name)?.Value;
                var email = context.User.FindFirst(ClaimTypes.Email)?.Value;
                _userService.CurrentUsers = new CurrentUser
                {
                    Uid = uid,
                    Username = name,
                    Email = email
                };
            }

            await _next.Invoke(context);
        }
    }
}
