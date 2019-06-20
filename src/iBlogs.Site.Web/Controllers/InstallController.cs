using System.Threading.Tasks;
using iBlogs.Site.Core.Common;
using iBlogs.Site.Core.Common.Extensions;
using iBlogs.Site.Core.Common.Response;
using iBlogs.Site.Core.Install.DTO;
using iBlogs.Site.Core.Install.Service;
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
            var installed = _configuration["DbInstalled"].ToBool();
            return View(new ViewBaseModel
            {
                Installed = installed
            });
        }

        [HttpPost]
        public async Task<ApiResponse<int>> Index(InstallParam param)
        {
            var installed = _configuration["DbInstalled"].ToBool();
            if (!param.AdminPwd.IsNullOrWhiteSpace() && !installed)
            {
                if (await _installService.InitializeDb(param))
                {
                    return ApiResponse<int>.Ok();
                }
            }
            return ApiResponse<int>.Fail("安装失败");
        }
    }
}