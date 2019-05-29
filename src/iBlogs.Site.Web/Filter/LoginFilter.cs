using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iBlogs.Site.Core.Common;
using iBlogs.Site.Core.Common.Extensions;
using iBlogs.Site.Core.User.DTO;
using iBlogs.Site.Core.User.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;

namespace iBlogs.Site.Web.Filter
{
    public class LoginFilter : IAuthorizationFilter
    {
        private readonly IUserService _userService;

        public LoginFilter(IUserService userService)
        {
            _userService = userService;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if(context.Filters.All(f=>!(f is AuthorizeFilter)))
                return;
            if (context.HttpContext.User.Claims.Any())
            {
                var uid = int.Parse(context.HttpContext.User.FindFirst(ClaimTypes.Uid)?.Value);
                var token = context.HttpContext.User.FindFirst(ClaimTypes.Token)?.Value;
                if (LoginStaticToken.CheckToken(uid, token))
                {
                    if (_userService.CurrentUsers.Username.IsNullOrWhiteSpace())
                    {
                        var user = _userService.FindUserById(uid);
                        _userService.CurrentUsers.Uid = uid;
                        _userService.CurrentUsers.Email = user.Email;
                        _userService.CurrentUsers.Username = user.Username;
                        _userService.CurrentUsers.ScreenName = user.ScreenName;
                        return;
                    }
                }
            }

            context.Result = new UnauthorizedResult();
        }
    }
}
