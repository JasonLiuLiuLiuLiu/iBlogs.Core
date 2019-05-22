using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iBlogs.Site.Core;
using iBlogs.Site.Core.Common;
using iBlogs.Site.Core.Common.DTO;
using iBlogs.Site.Core.Common.Extensions;
using iBlogs.Site.Core.Common.Service;
using iBlogs.Site.Core.Install;
using iBlogs.Site.Core.Option.Service;
using iBlogs.Site.Core.Service;
using iBlogs.Site.Core.User;
using iBlogs.Site.Core.User.Service;
using iBlogs.Site.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace iBlogs.Site.Web.Controllers
{
    public class InstallController : Controller
    {
        private readonly IInstallService _installService;
        private readonly IConfiguration _configuration;
        private readonly ISiteService _siteService;
        private readonly IUserService _userService;
        private readonly IOptionService _optionService;

        public InstallController(IInstallService installService, IConfiguration configuration, ISiteService siteService, IOptionService optionService, IUserService userService)
        {
            _installService = installService;
            _configuration = configuration;
            _siteService = siteService;
            _optionService = optionService;
            _userService = userService;
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
                {
                    Users temp = new Users
                    {
                        Username = param.AdminUser, Password =param.AdminPwd, Email = param.AdminEmail
                    };
                    if (!_userService.InsertUser(temp))
                    {
                        return RestResponse<int>.fail("安装失败");
                    }
                    var siteUrl = IBlogsUtils.buildURL(param.SiteUrl);
                    _optionService.saveOption("site_title", param.SiteTitle);
                    _optionService.saveOption("site_url", siteUrl);
                    return RestResponse<int>.ok();
                }
            }
            return RestResponse<int>.fail("安装失败");
        }
    }
}