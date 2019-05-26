using System;
using System.IO;
using System.Threading.Tasks;
using iBlogs.Site.Core.Common;
using iBlogs.Site.Core.Common.Response;
using iBlogs.Site.Core.EntityFrameworkCore;
using iBlogs.Site.Core.Install.DTO;
using iBlogs.Site.Core.Option.Service;
using iBlogs.Site.Core.User;
using iBlogs.Site.Core.User.Service;

namespace iBlogs.Site.Core.Install.Service
{
    public class InstallService : IInstallService
    {

        private readonly IOptionService _optionService;
        private readonly IUserService _userService;
        private readonly iBlogsContext _blogsContext;
        private InstallParam _param;

        public InstallService(IOptionService optionService, IUserService userService, iBlogsContext blogsContext)
        {
            _optionService = optionService;
            _userService = userService;
            _blogsContext = blogsContext;
        }

        public async Task<bool> InitializeDb(InstallParam param)
        {
            _param = param;
            try
            {
                var connectString = $"Server={param.DbUrl};Database={param.DbName};uid={param.DbUserName};pwd={param.DbUserPwd}";
                ConfigDataHelper.UpdateConnectionString(ConfigKey.BlogsConnectionString,connectString);
                await  _blogsContext.Database.EnsureCreatedAsync();
                Seed();
                ConfigDataHelper.UpdateDbInstallStatus(true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
           
            return true;
        }

        private bool Seed()
        {
            return CreateUser() && InitOptions();
        }

        private bool CreateUser()
        {
            Users temp = new Users
            {
                Username = _param.AdminUser,
                Password = _param.AdminPwd,
                Email = _param.AdminEmail
            };
          return  _userService.InsertUser(temp);
        }

        private bool InitOptions()
        {
            var siteUrl = IBlogsUtils.buildURL(_param.SiteUrl);
            _optionService.saveOption("site_title", _param.SiteTitle);
            _optionService.saveOption("site_url", siteUrl);
            return true;
        }

    }
}
