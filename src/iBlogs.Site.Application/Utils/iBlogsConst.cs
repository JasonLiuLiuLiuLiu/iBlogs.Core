using System;
using System.Collections.Generic;
using System.Text;
using iBlogs.Site.Application.Dto;

namespace iBlogs.Site.Application.Utils
{
    public static class iBlogsConst
    {
        public const string CLASSPATH = "";

        public const string REMEMBER_IN_COOKIE = "remember_me";
        public const string LOGIN_ERROR_COUNT = "login_error_count";
        public static string LOGIN_SESSION_KEY = "login_user";
        public static string REMEMBER_TOKEN = "";
        public static Boolean INSTALLED = false;
        public static Boolean ENABLED_CDN = true;

        /**
         * 最大页码
         */
        public const int MAX_PAGE = 100;

        /**
         * 最大获取文章条数
         */
        public const int MAX_POSTS = 9999;

        /**
         * 文章最多可以输入的文字数
         */
        public const int MAX_TEXT_COUNT = 200000;

        /**
         * 文章标题最多可以输入的文字个数
         */
        public const int MAX_TITLE_COUNT = 200;

        /**
         * 插件菜单
         */
        public static readonly List<PluginMenu> PLUGIN_MENUS = new List<PluginMenu>();

        /**
         * 上传文件最大20M
         */
        public static int MAX_FILE_SIZE = 204800;

        /**
         * 要过滤的ip列表
         */
        public static readonly List<string> BLOCK_IPS = new List<string>();

        public const string SLUG_HOME = "/";
        public const string SLUG_ARCHIVES = "archives";
        public const string SLUG_CATEGRORIES = "categories";
        public const string SLUG_TAGS = "tags";

        /**
         * 静态资源URI
         */
        public const string STATIC_URI = "/static";

        /**
         * 安装页面URI
         */
        public const string INSTALL_URI = "/install";

        /**
         * 后台URI前缀
         */
        public const string ADMIN_URI = "/admin";

        /**
         * 后台登录地址
         */
        public const string LOGIN_URI = "/admin/login";

        /**
         * 插件菜单 Attribute Name
         */
        public const string PLUGINS_MENU_NAME = "plugin_menus";

        public const string ENV_SUPPORT_163_MUSIC = "app.support_163_music";
        public const string ENV_SUPPORT_GIST = "app.support_gist";
        public const string MP3_PREFIX = "[mp3:";
        public const string MUSIC_IFRAME = "<iframe frameborder=\"no\" border=\"0\" marginwidth=\"0\" marginheight=\"0\" width=350 height=106 src=\"//music.163.com/outchain/player?type=2&id=$1&auto=0&height=88\"></iframe>";
        public const string MUSIC_REG_PATTERN = "\\[mp3:(\\d+)\\]";
        public const string GIST_PREFIX_URL = "https://gist.github.com/";
        public const string GIST_REG_PATTERN = "&lt;script src=\"https://gist.github.com/(\\w+)/(\\w+)\\.js\">&lt;/script>";
        public const string GIST_REPLATE_PATTERN = "<script src=\"https://gist.github.com/$1/$2\\.js\"></script>";


        public const string SQL_QUERY_METAS = "select a.*, count(b.cid) as count from t_metas a left join `t_relationships` b on a.mid = b.mid " +
                "where a.type = ? and a.name = ? group by a.mid";

        public const string SQL_QUERY_ARTICLES = "select a.* from t_contents a left join t_relationships b on a.cid = b.cid " +
                "where b.mid = ? and a.status = 'publish' and a.type = 'post' order by a.created desc";

        public const string COMMENT_APPROVED = "approved";
        public const string COMMENT_NO_AUDIT = "no_audit";

        public const string OPTION_CDN_URL = "cdn_url";
        public const string OPTION_SITE_THEME = "site_theme";
        public const string OPTION_ALLOW_INSTALL = "allow_install";
        public const string OPTION_ALLOW_COMMENT_AUDIT = "allow_comment_audit";
        public const string OPTION_ALLOW_CLOUD_CDN = "allow_cloud_CDN";
        public const string OPTION_SAFE_REMEMBER_ME = "safe_remember_me";

        public const string TEMPLATES = "/templates/";
        public const string THEME = "default";
    }
}
