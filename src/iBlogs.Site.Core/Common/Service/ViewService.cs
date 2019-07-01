using iBlogs.Site.Core.Common.Extensions;
using iBlogs.Site.Core.Option.Service;
using iBlogs.Site.Core.User.DTO;
using iBlogs.Site.Core.User.Service;
using System.Text;
using iBlogs.Site.Core.Option;
using Microsoft.Extensions.Configuration;

namespace iBlogs.Site.Core.Common.Service
{
    public class ViewService : IViewService
    {
        private readonly IOptionService _optionService;
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public ViewService(IOptionService optionService, IUserService userService, IConfiguration configuration)
        {
            _optionService = optionService;
            _userService = userService;
            _configuration = configuration;
        }

        public CurrentUser User => _userService.CurrentUsers;

        /**
         * 获取header keywords
         *
         * @return
         */

        public string MetaKeywords()
        {
            var value = _optionService.Get(Option.ConfigKey.Keywords);
            if (null != value)
            {
                return value;
            }
            return _optionService.Get(Option.ConfigKey.SiteKeywords);
        }

        /**
         * 获取header description
         *
         * @return
         */

        public string MetaDescription()
        {
            var value = _optionService.Get(Option.ConfigKey.Description);
            if (null != value)
            {
                return value;
            }
            return _optionService.Get(Option.ConfigKey.SiteDescription);
        }


        public string SiteOption(ConfigKey key)
        {
            return _optionService.Get(key);
        }

        /**
         * 返回社交账号链接
         *
         * @param type
         * @return
         */

        public string SocialLink(ConfigKey key)
        {
            string id = SiteOption(key);
            switch (key)
            {
                case ConfigKey.Github:
                    return "https://github.com/" + id;

                case ConfigKey.WeiBo:
                    return "http://weibo.com/" + id;

                case ConfigKey.Twitter:
                    return "https://twitter.com/" + id;

                case ConfigKey.ZhiHu:
                    return "https://www.zhihu.com/people/" + id;

                default:
                    return null;
            }
        }

        /**
         * 返回网站首页链接，如：http://tale.biezhi.me
         *
         * @return
         */

        public string SiteUrl()
        {
            return SiteUrl("");
        }

        /**
         * 返回网站链接下的全址
         *
         * @param sub 后面追加的地址
         * @return
         */

        public string SiteUrl(string sub)
        {
            return SiteOption(Option.ConfigKey.SiteUrl) + sub;
        }

        /**
         * 网站子标题
         *
         * @return
         */

        public string SiteSubtitle()
        {
            return SiteOption(Option.ConfigKey.SiteSubtitle);
        }

        /**
         * 是否允许使用云公共静态资源
         *
         * @return
         */

        public string AllowCloudCdn()
        {
            return SiteOption(Option.ConfigKey.AllowCloudCdn);
        }

        /**
         * 网站配置项
         *
         * @param key
         * @param defaultValue 默认值
         * @return
         */

        public string SiteOption(ConfigKey key, string defaultValue)
        {
            return ConfigData.Get(key, defaultValue);
        }

        /**
         * 返回站点设置的描述信息
         *
         * @return
         */

        public string SiteDescription()
        {
            return SiteOption(Option.ConfigKey.SiteDescription);
        }

        /**
         * 返回主题下的文件路径
         *
         * @param sub
         * @return
         */

        public string ThemeUrl(string sub)
        {
            return SiteUrl(ConfigData.Get(ConfigKey.TemplesPath) + ConfigData.Get(ConfigKey.ThemePath) + sub);
        }

        /**
         * 返回gravatar头像地址
         *
         * @param email
         * @return
         */

        public string Gravatar(string email)
        {
            if (email.IsNullOrWhiteSpace())
                return "https://www.Gravatar.com/avatar";
            return $"https://www.Gravatar.com/avatar/{CreateMD5(email).ToLowerInvariant()}?s=60&d=blank";
        }

        private string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                foreach (var t in hashBytes)
                {
                    sb.Append(t.ToString("X2"));
                }
                return sb.ToString();
            }
        }


        public string AttachURL()
        {
            return _optionService.Get(Option.ConfigKey.AttachUrl, SiteUrl());
        }


        public string CdnURL()
        {
            return _optionService.Get(Option.ConfigKey.CdnUrl, "/static/admin");
        }

        public string BuildNumber()
        {
            return _configuration["BuildNumber"] ?? "20190701.01";
        }
    }
}