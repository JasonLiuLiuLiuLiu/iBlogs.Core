using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iBlogs.Site.Web.Attribute;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace iBlogs.Site.Web.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class SettingController : Controller
    {
        [ViewLayout("~/Areas/Admin/Views/Layout/Layout.cshtml")]
        public IActionResult Index()
        {
            return View();
        }
    }
}