using System;
using System.Collections.Generic;
using iBlogs.Site.Core.Option.Service;

namespace iBlogs.Site.Core.Option
{
    public class ConfigData
    {
        private static IOptionService _optionService;

        public static void Init(IOptionService optionService)
        {
            _optionService = optionService;
        }

        public static string Get(ConfigKey key, string defaultValue = null)
        {
            if (_optionService == null)
                throw new Exception("ConfigData使用前未进行初始化");
            return _optionService.Get(key, defaultValue);
        }

        public static IDictionary<ConfigKey, string> GetAll()
        {
            if (_optionService == null)
                throw new Exception("ConfigData使用前未进行初始化");
            return _optionService.GetAllAsKeyValue();
        }
    }
}
