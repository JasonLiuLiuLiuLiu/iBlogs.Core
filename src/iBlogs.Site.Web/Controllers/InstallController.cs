using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iBlogs.Site.Application;
using iBlogs.Site.Application.Extensions;
using iBlogs.Site.Application.Params;
using iBlogs.Site.Application.Response;
using iBlogs.Site.Application.Service;
using iBlogs.Site.Application.Service.Install;
using iBlogs.Site.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace iBlogs.Site.Web.Controllers
{
    public class InstallController : Controller
    {
        private readonly IInstallService _installService;
        private readonly IConfiguration _configuration;

        public InstallController(IInstallService installService, IConfiguration configuration)
        {
            _installService = installService;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var installed = _configuration[ConfigKey.DbInstalled].ToBool();
            return View(new ViewBaseModel
            {
                Installed = installed
            });
        }

        [HttpPost]
        public RestResponse<int> Index(InstallParam param)
        {
            
            var installed = _configuration[ConfigKey.DbInstalled].ToBool();
            if (!param.AdminPwd.IsNullOrWhiteSpace() && !installed)
            {
                if (_installService.InitializeDb())
                   return RestResponse<int>.ok();
            }
            return RestResponse<int>.fail("安装失败");
        }
    }
}