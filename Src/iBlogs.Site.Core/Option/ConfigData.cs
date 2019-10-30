using System.Collections.Generic;
using iBlogs.Site.Core.Common;
using iBlogs.Site.Core.Option.Service;

namespace iBlogs.Site.Core.Option
{
    public static class ConfigData
    {
        private static readonly IOptionService OptionService;

        static ConfigData()
        {
            OptionService = ServiceFactory.GetService<IOptionService>();
        }

        public static string Get(ConfigKey key, string defaultValue = null)
        {
            return OptionService.Get(key, defaultValue);
        }

        public static IDictionary<ConfigKey, string> GetAll()
        {
            return OptionService.GetAllAsKeyValue();
        }
    }
}
