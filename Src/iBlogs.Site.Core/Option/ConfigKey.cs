namespace iBlogs.Site.Core.Option
{
    public enum ConfigKey
    {
        [ConfigKey("默认")]
        Default,
        [ConfigKey("网站标题","iBlogs",true)]
        SiteTitle,
        [ConfigKey("附件地址")]
        AttachUrl,
        [ConfigKey("CdnUrl,谨慎设置",true)]
        CdnUrl,
        [ConfigKey("站点描述","a blogs system power by asp.net core", true)]
        Description,
        [ConfigKey("是否允许使用云CDN加速,如果启用公共的静态js,css会从公共CND加载", true)]
        AllowCloudCdn,
        [ConfigKey("站点子标题", true)]
        SiteSubtitle,
        [ConfigKey("网站地址", true)]
        SiteUrl,
        [ConfigKey("网站关键字", true)]
        Keywords,
        [ConfigKey("是否允许重新安装","true")]
        AllowInstall,
        [ConfigKey("Github地址", true)]
        Github,
        [ConfigKey("微博地址", true)]
        WeiBo,
        [ConfigKey("Twitter地址", true)]
        Twitter,
        [ConfigKey("知乎地址", true)]
        ZhiHu,
        [ConfigKey("评论是否需要审核", true)]
        AllowCommentAudit,
        [ConfigKey("博客署名作者名称,显示在文章底部","iBlogs", true)]
        Author,
        [ConfigKey("首页侧边栏标签显示数目", true)]
        SideBarTagsCount,
        [ConfigKey("首页侧边栏分类显示数目", true)]
        SideBarCategoriesCount,
        [ConfigKey("登录失败次数限制,尚未启用")]
        LoginErrorCount,
        [ConfigKey("每页显示的最大数量","100", true)]
        MaxPage,
        [ConfigKey("每页显示的内容的大小", "20", true)]
        PageSize,
        [ConfigKey("文章的最大字数", "2147483647", true)]
        MaxTextCount,
        [ConfigKey("标题最大字数","200", true)]
        MaxTitleCount,
        [ConfigKey("摘要字数","1000", true)]
        MaxIntroCount,
        [ConfigKey("上传文件大小限制", true)]
        MaxFileSize,
        [ConfigKey("限制ip列表(,)分隔,该功能未生效", true)]
        BlockIpList,
        [ConfigKey("静态文件地址", true)]
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
        [ConfigKey("博客园同步开关","false")]
        CnBlogsSyncSwitch,
        [ConfigKey("博客园用户名")]
        CnBlogsUserName,
        [ConfigKey("博客园密码")]
        CnBlogsPassword,
        [ConfigKey("博客园同步地址", "https://rpc.cnblogs.com/metaweblog/{YourBlogName}")]
        CnBlogsUrl,
        [ConfigKey("谷歌站点统计跟踪Id,https://analytics.google.com", true)]
        GoogleAnalyticId,
        [ConfigKey("百度统计Id,https://tongji.baidu.com", true)]
        BaiDuTongJiId,
        [ConfigKey("站点安装时间,用于首页显示网站运行时间",true)]
        SiteInstallTime,
        [ConfigKey("文章数目")]
        ContentCount,
        [ConfigKey("评论数目")]
        CommentCount,
        [ConfigKey("最后活动时间")]
        LastActiveTime,
        [ConfigKey("通知公告,支持HTML","请在后台管理系统中配置您的公告信息",true)]
        Announcement,
        [ConfigKey("页脚信息1,支持HTML", "请在后台管理系统中配置您的页脚信息1",true)]
        FootContent1,
        [ConfigKey("页脚信息2,支持HTML", "请后台管理系统中配置您的页脚信息2", true)]
        FootContent2,
        [ConfigKey("邮件通知From邮箱地址",true)]
        EmailFromAccount,
        [ConfigKey("邮箱服务器用户名",true)]
        EmailUserName,
        [ConfigKey("邮箱服务器密码",true)]
        EmailPassword,
        [ConfigKey("邮件服务器地址",true)]
        EmailSmtpHost,
        [ConfigKey("邮件服务器端口", "587",true)]
        EmailSmtpHostPort,

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
