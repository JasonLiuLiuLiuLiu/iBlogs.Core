using System;
using System.Collections.Generic;

namespace iBlogs.Site.Core.Service.Options
{
    public interface IOptionService
    {
        void ReLoad();
        void Set(string key, string value);
        string Get(string key, string defaultValue = null);
        void saveOption(String key, String value);
        IDictionary<string, string> getOptions();
        String getOption(String key);
        void deleteOption(string key);
    }
}