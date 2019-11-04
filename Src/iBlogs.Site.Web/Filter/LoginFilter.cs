using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using iBlogs.Site.Core.Common;
using iBlogs.Site.Core.Option;
using iBlogs.Site.Core.Option.Service;
using iBlogs.Site.Core.Security.DTO;
using iBlogs.Site.Core.Security.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace iBlogs.Site.Web.Filter
{
    public class LoginFilter : IAuthorizationFilter
    {
        private readonly IUserService _userService;
        private readonly IOptionService _optionService;
        private static Dictionary<string, List<string>> _authorizationRequiredAction;

        public LoginFilter(IUserService userService, IOptionService optionService)
        {
            _userService = userService;
            _optionService = optionService;

            if (_authorizationRequiredAction == null)
            {
                _authorizationRequiredAction = GetAllAuthorizationRequiredAction();
            }
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!NeedAuthorization(context.ActionDescriptor.RouteValues))
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

        private bool NeedAuthorization(IDictionary<string, string> routeValue)
        {
            if (!routeValue.TryGetValue("Controller", out var controller) || !routeValue.TryGetValue("Action", out var action))
                return false;
            routeValue.TryGetValue("Area", out var area);

            if (string.IsNullOrEmpty(controller) || string.IsNullOrEmpty(action))
                return false;

            var key = area == null ? controller : area + controller;

            return _authorizationRequiredAction.ContainsKey(key) && _authorizationRequiredAction[key].Contains(action);
        }

        private Dictionary<string, List<string>> GetAllAuthorizationRequiredAction()
        {

            return Assembly.GetAssembly(typeof(Program)).GetTypes()
                  .Where(type => typeof(Controller).IsAssignableFrom(type))
                  .Where(c => c.GetCustomAttributes().Any(a => a is AuthorizeAttribute) ||
                              c.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public)
                                  .Any(m => m.GetCustomAttributes().Any(a => a is AuthorizeAttribute))).ToList()
                  .ToDictionary(c =>
                  {
                      var area = (AreaAttribute)c.GetCustomAttributes().FirstOrDefault(a => a is AreaAttribute);
                      var controllerName = c.Name.Replace("Controller", "");
                      if (area != null)
                          return area.RouteValue + controllerName;
                      return controllerName;
                  }, c =>
                  {
                      var allMethod =
                          c.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public);
                      if (c.GetCustomAttributes().Any(a => a is AuthorizeAttribute))
                          return allMethod.Select(m => m.Name).ToList();
                      return allMethod.Where(u => u.GetCustomAttributes().Any(a => a is AuthorizeAttribute))
                          .Select(m => m.Name).ToList();
                  });
        }
    }
}
