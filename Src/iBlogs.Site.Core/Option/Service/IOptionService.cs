using System.Collections.Generic;

namespace iBlogs.Site.Core.Option.Service
{
    public interface IOptionService
    {
        void Load();
        IDictionary<ConfigKey, string> GetAll();
        void Set(ConfigKey key, string value, string description = null);
        string Get(ConfigKey key, string defaultValue = null);
    }
}