using iBlogs.Site.Core.Common;
using iBlogs.Site.Core.EntityFrameworkCore;
using iBlogs.Site.Core.Install.DTO;
using iBlogs.Site.Core.Option.Service;
using System;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using iBlogs.Site.Core.Blog.Content;
using iBlogs.Site.Core.Blog.Content.DTO;
using iBlogs.Site.Core.Blog.Content.Service;
using iBlogs.Site.Core.Blog.Meta;
using iBlogs.Site.Core.Blog.Meta.Service;
using iBlogs.Site.Core.Security;
using iBlogs.Site.Core.Security.Service;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using ConfigKey = iBlogs.Site.Core.Option.ConfigKey;

namespace iBlogs.Site.Core.Install.Service
{
    public class InstallService : IInstallService
    {
        private readonly IOptionService _optionService;
        private readonly IUserService _userService;
        private readonly iBlogsContext _blogsContext;
        private readonly ITransactionProvider _transactionProvider;
        private readonly IContentsService _contentsService;
        private readonly IMetasService _metasService;
        private InstallParam _param;
        private Users _users;
        private readonly IApplicationLifetime _applicationLifetime;

        public InstallService(IOptionService optionService, IUserService userService, iBlogsContext blogsContext, ITransactionProvider transactionProvider, IContentsService contentsService, IMetasService metasService, IApplicationLifetime applicationLifetime)
        {
            _optionService = optionService;
            _userService = userService;
            _blogsContext = blogsContext;
            _transactionProvider = transactionProvider;
            _contentsService = contentsService;
            _metasService = metasService;
            _applicationLifetime = applicationLifetime;
        }

        public void WriteInstallInfo(InstallParam param)
        {
            var connectString = $"Server={param.DbUrl};Database={param.DbName};uid={param.DbUserName};pwd={param.DbUserPwd}";
            ConfigDataHelper.UpdateConnectionString("iBlogs", connectString);
            ConfigDataHelper.UpdateRedisConfig(param.RedisHost, param.RedisPwd);
            ConfigDataHelper.UpdateRabbitMqConfig(param.RabbitMqHost, param.RabbitMqUid, param.RabbitMqPwd);
            ConfigDataHelper.SaveInstallParam(param);
            Task.Run(() =>
            {
                Thread.Sleep(1000);
                _applicationLifetime.StopApplication();
            });
        }

        public async Task<bool> InitializeDb()
        {

            try
            {
                _param = ConfigDataHelper.ReadInstallParam();
                await InitDbFromScript();
                Seed();
                ConfigDataHelper.UpdateDbInstallStatus(true);
                ConfigDataHelper.DeleteInstallParamFile();
                _optionService.Set(ConfigKey.SiteInstallTime, DateTime.Now.ToString(CultureInfo.InvariantCulture));
                _optionService.Set(ConfigKey.LastActiveTime, DateTime.Now.ToString(CultureInfo.InvariantCulture));
                _optionService.Load();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return false;
        }

        private async Task InitDbFromScript()
        {
            try
            {
                var sqlScript = File.ReadAllText("InitDb.sql");
                await _blogsContext.Database.ExecuteSqlCommandAsync(sqlScript);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private void Seed()
        {
            using (var tra = _transactionProvider.CreateTransaction())
            {
                var result = CreateUser() && InitOptions() && InitContent();
                if (result)
                    tra.Commit();
                else
                    tra.Rollback();
            }
        }

        private bool CreateUser()
        {
            _users = new Users
            {
                Username = _param.AdminUser,
                Password = _param.AdminPwd,
                Email = _param.AdminEmail
            };
            return _userService.InsertUser(_users);
        }

        private bool InitOptions()
        {
            _optionService.Set(ConfigKey.AllowInstall, "false", "是否允许重新安装博客");
            _optionService.Set(ConfigKey.AllowCommentAudit, "true", "评论需要审核");
            _optionService.Set(ConfigKey.Keywords, "博客系统,asp.net core,iBlogs");
            _optionService.Set(ConfigKey.Description, "博客系统,asp.net core,iBlogs");
            var siteUrl = BlogsUtils.BuildUrl(_param.SiteUrl);
            _optionService.Set(ConfigKey.SiteTitle, _param.SiteTitle);
            _optionService.Set(ConfigKey.SiteUrl, siteUrl);
            _optionService.Set(ConfigKey.MaxPage, 100.ToString());
            _optionService.Set(ConfigKey.MaxTextCount, 200000.ToString());
            _optionService.Set(ConfigKey.MaxTitleCount, 200.ToString());
            _optionService.Set(ConfigKey.MaxIntroCount, 500.ToString());
            _optionService.Set(ConfigKey.MaxFileSize, 204800.ToString());
            _optionService.Set(ConfigKey.StaticUrl, "/static");
            _optionService.Set(ConfigKey.TemplesPath, "/templates/");
            _optionService.Set(ConfigKey.ThemePath, "themes/default");
            return true;
        }

        private bool InitContent()
        {
            _contentsService.Publish(new ContentInput
            {
                Title = "关于",
                Slug = "about",
                Created = DateTime.Now,
                Modified = DateTime.Now,
                Content = "### Hello World\r\n\r\n这是我的关于页面\r\n\r\n### 当然还有其他\r\n\r\n具体你来写点什么吧",
                AuthorId = _users.Id,
                Type = ContentType.Page,
                Status = ContentStatus.Publish,
                Categories = "默认分类",
                Hits = 0,
                CommentsNum = 0,
                AllowComment = true,
                AllowPing = true,
                AllowFeed = true
            });

            var firstArticle = _contentsService.Publish(new ContentInput
            {
                Title = "第一篇文章",
                Created = DateTime.Now,
                Modified = DateTime.Now,
                Content = "## Hello  World.\r\n\r\n> 第一篇文章总得写点儿什么?...\r\n\r\n----------\r\n\r\n\r\n<!--more-->\r\n\r\n```java\r\npublic static void main(string[] args){\r\n    System.out.println(\\\"Hello Tale.\\\");\r\n}\r\n```",
                AuthorId = _users.Id,
                Type = ContentType.Post,
                Status = ContentStatus.Publish,
                Categories = "默认分类",
                Hits = 10,
                CommentsNum = 0,
                AllowComment = true,
                AllowPing = true,
                AllowFeed = true
            });

            _contentsService.Publish(new ContentInput
            {
                Title = "友情链接",
                Slug = "links",
                Created = DateTime.Now,
                Modified = DateTime.Now,
                Content = "## 友情链接\r\n\r\n- :lock: [码农阿宇(博客园)](https://www.cnblogs.com/coderayu)\r\n- :lock: [刘振宇(Github)](https://github.com/liuzhenyulive)\r\n- :lock: [iBlogs](https://iblogs.site)\r\n\r\n## 链接须知\r\n\r\n> 请确定贵站可以稳定运营\r\n> 原创博客优先，技术类博客优先，设计、视觉类博客优先\r\n> 经常过来访问和评论，眼熟的\r\n\r\n备注：默认申请友情链接均为内页（当前页面）\r\n\r\n## 基本信息\r\n\r\n                网站名称：iBlogs\r\n                网站地址：https://iblogs.site\r\n\r\n请在当页通过评论来申请友链，其他地方不予回复\r\n\r\n暂时先这样，同时欢迎互换友链，这个页面留言即可。 ^_^\r\n\r\n还有，我会不定时对无法访问的网址进行清理，请保证自己的链接长期有效。'",
                AuthorId = _users.Id,
                Type = ContentType.Page,
                Status = ContentStatus.Publish,
                Categories = "默认分类",
                Hits = 10,
                CommentsNum = 0,
                AllowComment = true,
                AllowPing = true,
                AllowFeed = true
            });
            _metasService.SaveMetas(firstArticle, "默认分类", MetaType.Category);
            return true;
        }
    }
}