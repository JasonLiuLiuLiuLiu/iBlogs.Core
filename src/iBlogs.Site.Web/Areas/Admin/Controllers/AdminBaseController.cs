using iBlogs.Site.Core.Service.Users;
using iBlogs.Site.Core.Utils.Extensions;
using iBlogs.Site.Web.Attribute;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace iBlogs.Site.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class AdminBaseController : Controller
    {
        protected readonly IUserService UserService;

        public AdminBaseController(IUserService userService)
        {
            UserService = userService;
            UserService.CurrentUsers = HttpContext.CurrentUser();
        }
    }
}