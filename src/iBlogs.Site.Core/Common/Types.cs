namespace iBlogs.Site.Core.Common
{
    public static class Types
    {
        public static string TAG = "tag";
        public static string CATEGORY = "category";
        public static string ARTICLE = "post";
        public static string PUBLISH = "publish";
        public static string PAGE = "page";
        public static string DRAFT = "draft";
        public static string IMAGE = "image";
        public static string FILE = "file";
        public static string COMMENTS_FREQUENCY = "comments:frequency";

        public static string RECENT_ARTICLE = "recent_article";
        public static string RANDOM_ARTICLE = "random_article";

        public static string RECENT_META = "recent_meta";
        public static string RANDOM_META = "random_meta";

        public static string SYS_STATISTICS = "sys:statistics";

        public static string NEXT = "next";
        public static string PREV = "prev";

        /**
         * 附件存放的URL，默认为网站地址，如集成第三方则为第三方CDN域名
         */
        public static string ATTACH_URL = "attach_url";
        public static string CDN_URL = "cdn_url";

        /**
         * 网站要过滤，禁止访问的ip列表
         */
        public static string BLOCK_IPS = "site_block_ips";
    }
}