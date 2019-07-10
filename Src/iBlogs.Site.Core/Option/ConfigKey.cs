namespace iBlogs.Site.Core.Option
{
    public enum ConfigKey
    {
        Default,
        ConfigLoaded,
        SiteTitle,
        AttachUrl,
        CdnUrl,
        Description,
        SiteDescription,
        AllowCloudCdn,
        SiteSubtitle,
        SiteUrl,
        Keywords,
        SiteKeywords,
        AllowInstall,
        SocialPre,
        AllowCommentAudit,
        Author,
        SideBarTagsCount,
        SideBarCategoriesCount,
        LoginErrorCount,
        Installed,
        MaxPage,
        MaxTextCount,
        MaxTitleCount,
        MaxIntroCount,
        MaxFileSize,
        BlockIpList,
        StaticUrl,
        TemplesPath,
        ThemePath,
        Github,
        WeiBo,
        Twitter,
        ZhiHu,
        GithubWebHookSecret
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
