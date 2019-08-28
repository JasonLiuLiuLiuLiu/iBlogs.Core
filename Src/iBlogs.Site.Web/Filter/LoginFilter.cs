using System;
using System.Globalization;
using System.Linq;
using iBlogs.Site.Core.Common;
using iBlogs.Site.Core.Option;
using iBlogs.Site.Core.Option.Service;
using iBlogs.Site.Core.Security.DTO;
using iBlogs.Site.Core.Security.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;

namespace iBlogs.Site.Web.Filter
{
    public class LoginFilter : IAuthorizationFilter
    {
        private readonly IUserService _userService;
        private readonly IOptionService _optionService;

        public LoginFilter(IUserService userService, IOptionService optionService)
        {
            _userService = userService;
            _optionService = optionService;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.Filters.All(f => !(f is AuthorizeFilter)))
                return;
            if (context.HttpContext.User.Claims.Any())
            {
                var uid = int.Parse(context.HttpContext.User.FindFirst(ClaimTypes.Uid)?.Value ?? throw new InvalidOperationException());
                var token = context.HttpContext.User.FindFirst(ClaimTypes.Token)?.Value;
                if (LoginToken.CheckToken(uid, token))
                {
                    if (_userService.CurrentUsers == null)
                    {
                        var user = _userService.FindUserById(uid);
                        _userService.CurrentUsers = new CurrentUser
                        {
                            Uid = uid,
                            Email = user.Email,
                            Username = user.Username,
                            ScreenName = user.ScreenName,
                            HomeUrl = user.HomeUrl
                        };

                        _optionService.Set(ConfigKey.LastActiveTime, DateTime.Now.ToString(CultureInfo.InvariantCulture));

                        return;
                    }
                }
            }

            context.Result = new UnauthorizedResult();
        }
    }
}
