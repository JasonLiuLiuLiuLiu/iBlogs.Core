using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iBlogs.Site.Application.Extensions;
using iBlogs.Site.Application.Params;
using iBlogs.Site.Application.Service;
using iBlogs.Site.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace iBlogs.Site.Web.Controllers
{
    public class InstallController : Controller
    {
        private IInstallService _installService;

        public InstallController(IInstallService installService)
        {
            _installService = installService;
        }

        public IActionResult Index(InstallParam param)
        {
            if (param.AdminPwd.IsNullOrWhiteSpace())
            {
                _installService.InitializeDbAsync();
                return Redirect("/Home/Index");
            }
            else
            {
                return View(new ViewBaseModel());
            }
        }
    }
}