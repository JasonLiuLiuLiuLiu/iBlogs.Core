using iBlogs.Site.Core.Common.Extensions;
using iBlogs.Site.Core.Option.Service;
using iBlogs.Site.Core.User.DTO;
using iBlogs.Site.Core.User.Service;
using System.Text;
using iBlogs.Site.Core.Option;

namespace iBlogs.Site.Core.Common.Service
{
    public class ViewService : IViewService
    {
        private readonly IOptionService _optionService;
        private readonly IUserService _userService;

        public ViewService(IOptionService optionService,IUserService userService)
        {
            _optionService = optionService;
            _userService = userService;
        }
        public CurrentUser User => _userService.CurrentUsers;

        /**
         * 获取header keywords
         *
         * @return
         */

        public string MetaKeywords()
        {
            var value = _optionService.Get(OptionKeys.Keywords);
            if (null != value)
            {
                return value;
            }
            return _optionService.Get(OptionKeys.SiteKeywords);
        }

        /**
         * 获取header description
         *
         * @return
         */

        public string MetaDescription()
        {
            var value = _optionService.Get(OptionKeys.Description);
            if (null != value)
            {
                return value;
            }
            return _optionService.Get(OptionKeys.SiteDescription);
        }


        public string SiteOption(string key)
        {
            return _optionService.Get(key);
        }

        /**
         * 返回社交账号链接
         *
         * @param type
         * @return
         */

        public string SocialLink(string type)
        {
            string id = SiteOption(OptionKeys.SocialPre + type);
            switch (type)
            {
                case "github":
                    return "https://github.com/" + id;

                case "weibo":
                    return "http://weibo.com/" + id;

                case "twitter":
                    return "https://twitter.com/" + id;

                case "zhihu":
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
            return SiteOption(OptionKeys.SiteUrl) + sub;
        }

        /**
         * 网站子标题
         *
         * @return
         */

        public string SiteSubtitle()
        {
            return SiteOption(OptionKeys.SiteSubtitle);
        }

        /**
         * 是否允许使用云公共静态资源
         *
         * @return
         */

        public string AllowCloudCdn()
        {
            return SiteOption(OptionKeys.AllowCloudCdn);
        }

        /**
         * 网站配置项
         *
         * @param key
         * @param defaultValue 默认值
         * @return
         */

        public string SiteOption(string key, string defaultValue)
        {
            if (stringKit.isBlank(key))
            {
                return "";
            }
            return _optionService.Get(key, defaultValue);
        }

        /**
         * 返回站点设置的描述信息
         *
         * @return
         */

        public string SiteDescription()
        {
            return SiteOption(OptionKeys.SiteDescription);
        }

        /**
         * 返回主题下的文件路径
         *
         * @param sub
         * @return
         */

        public string ThemeUrl(string sub)
        {
            return SiteUrl(iBlogsConfig.TEMPLATES + iBlogsConfig.THEME + sub);
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
            return _optionService.Get(OptionKeys.AttachUrl, SiteUrl());
        }


        public string CdnURL()
        {
            return _optionService.Get(OptionKeys.CdnUrl, "/static/admin");
        }
    }
}