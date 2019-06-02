using iBlogs.Site.Core.User.DTO;

namespace iBlogs.Site.Core.Common.Service
{
    public interface IViewService
    {
        CurrentUser User { get; }
        string MetaKeywords();
        string MetaDescription();
        string SiteOption(string key);
        string SocialLink(string type);
        string SiteUrl();
        string SiteUrl(string sub);
        string SiteSubtitle();
        string AllowCloudCdn();
        string SiteOption(string key, string defaultValue);
        string SiteDescription();
        string ThemeUrl(string sub);
        string Gravatar(string email);
        string AttachURL();
        string CdnURL();
    }
}