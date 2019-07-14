using System.Collections.Generic;
using iBlogs.Site.Core.Option.DTO;

namespace iBlogs.Site.Core.Option.Service
{
    public interface IOptionService
    {
        void Load();
        IDictionary<ConfigKey, string> GetAllAsKeyValue();
        void Set(ConfigKey key, string value, string description = null);
        string Get(ConfigKey key, string defaultValue = null);
        List<OptionParam> GetAll();
    }
}