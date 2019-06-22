namespace iBlogs.Site.Core.Option.Service
{
    public interface IOptionService
    {
        void Load();
        void Set(ConfigKey key, string value, string description = null);
        string Get(ConfigKey key, string defaultValue = null);
    }
}