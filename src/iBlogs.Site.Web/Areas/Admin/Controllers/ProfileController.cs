using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iBlogs.Site.Core.Common.Response;
using iBlogs.Site.Core.User.DTO;
using iBlogs.Site.Core.User.Service;
using iBlogs.Site.Web.Attribute;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace iBlogs.Site.Web.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class ProfileController : Controller
    {
        private readonly IUserService _userService;

        public ProfileController(IUserService userService)
        {
            _userService = userService;
        }

        [ViewLayout("~/Areas/Admin/Views/Layout/Layout.cshtml")]
        public IActionResult Index()
        {
            return View();
        }

      
        public ApiResponse Info(UpdateUserParam param)
        {
            _userService.UpdateUserInfo(param);
            return ApiResponse.Ok();
        }

        public ApiResponse Password(PwdUpdateParam param)
        {
            _userService.UpdatePwd(param);
            return ApiResponse.Ok();
        }
    }
}