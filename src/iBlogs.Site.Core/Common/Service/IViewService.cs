using iBlogs.Site.Core.Option;
using iBlogs.Site.Core.User.DTO;

namespace iBlogs.Site.Core.Common.Service
{
    public interface IViewService
    {
        CurrentUser User { get; }
        string MetaKeywords();
        string MetaDescription();
        string SiteOption(ConfigKey key);
        string SocialLink(ConfigKey key);
        string SiteUrl();
        string SiteUrl(string sub);
        string SiteSubtitle();
        string AllowCloudCdn();
        string SiteOption(ConfigKey key, string defaultValue);
        string SiteDescription();
        string ThemeUrl(string sub);
        string Gravatar(string email);
        string AttachURL();
        string CdnURL();
    }
}