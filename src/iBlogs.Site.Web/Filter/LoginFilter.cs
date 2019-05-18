using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iBlogs.Site.Core.Dto;
using iBlogs.Site.Core.Service.Users;
using Microsoft.AspNetCore.Mvc.Filters;

namespace iBlogs.Site.Web.Filter
{
    public class LoginFilter: IAuthorizationFilter
    {
        private readonly IUserService _userService;

        public LoginFilter(IUserService userService)
        {
            _userService = userService;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.HttpContext.User.Claims.Any())
            {
                var uid = int.Parse(context.HttpContext.User.FindFirst(ClaimTypes.Uid)?.Value);
                var name = context.HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
                var email = context.HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
                _userService.CurrentUsers = new CurrentUser
                {
                    Uid = uid,
                    Username = name,
                    Email = email
                };
            }
        }
    }
}
