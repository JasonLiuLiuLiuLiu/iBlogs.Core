namespace iBlogs.Site.Core.Option
{
    public enum ConfigKey
    {
        [ConfigKey("默认")]
        Default,
        [ConfigKey("网站标题","iBlogs")]
        SiteTitle,
        [ConfigKey("附件地址")]
        AttachUrl,
        [ConfigKey("CdnUrl,谨慎设置")]
        CdnUrl,
        [ConfigKey("站点描述","a blogs system power by asp.net core")]
        Description,
        [ConfigKey("是否允许使用云CDN加速,如果启用公共的静态js,css会从公共CND加载")]
        AllowCloudCdn,
        [ConfigKey("站点子标题")]
        SiteSubtitle,
        [ConfigKey("网站地址")]
        SiteUrl,
        [ConfigKey("网站关键字")]
        Keywords,
        [ConfigKey("是否允许重新安装","true")]
        AllowInstall,
        [ConfigKey("Github地址")]
        Github,
        [ConfigKey("微博地址")]
        WeiBo,
        [ConfigKey("Twitter地址")]
        Twitter,
        [ConfigKey("知乎地址")]
        ZhiHu,
        [ConfigKey("评论是否需要审核")]
        AllowCommentAudit,
        [ConfigKey("博客署名作者名称,显示在文章底部","iBlogs")]
        Author,
        [ConfigKey("首页侧边栏标签显示数目")]
        SideBarTagsCount,
        [ConfigKey("首页侧边栏分类显示数目")]
        SideBarCategoriesCount,
        [ConfigKey("登录失败次数限制,尚未启用")]
        LoginErrorCount,
        [ConfigKey("每页显示的最大数量","100")]
        MaxPage,
        [ConfigKey("文章的最大字数", "2147483647")]
        MaxTextCount,
        [ConfigKey("标题最大字数","200")]
        MaxTitleCount,
        [ConfigKey("摘要字数","1000")]
        MaxIntroCount,
        [ConfigKey("上传文件大小限制")]
        MaxFileSize,
        [ConfigKey("限制ip列表(,)分隔,未生效")]
        BlockIpList,
        [ConfigKey("静态文件地址")]
        StaticUrl,
        [ConfigKey("模板地址")]
        TemplesPath,
        [ConfigKey("主题地址")]
        ThemePath,

        [ConfigKey("Git钩子暗号")]
        GitWebHookSecret,
        [ConfigKey("Git文章仓库地址")]
        GitProjectCloneUrl,
        [ConfigKey("Git用户名")]
        GitUerName,
        [ConfigKey("Git密码")]
        GitPassword,
        [ConfigKey("Git提交者名称,该用户提交后Git钩子通知本系统不再重复处理","iBlogs")]
        GitCommitter,
        [ConfigKey("Git文章提交以该用户为作者","1")]
        GitAuthorId,
        [ConfigKey("是否同步到博客园","false")]
        SyncToCnBlogs,
        [ConfigKey("博客园同步开关","false")]
        CnBlogsSyncSwitch,
        [ConfigKey("博客园用户名")]
        CnBlogsUserName,
        [ConfigKey("博客园密码")]
        CnBlogsPassword
    }


    public static class ConfigKeyExtension
    {
        private const string KeyPre = "IBLOGS_OPTION_KEY_PRE";

        public static string ToCacheKey(this ConfigKey config)
        {
            return KeyPre + config;
        }
    }
}
