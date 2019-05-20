using System;
using System.Collections.Generic;

namespace iBlogs.Site.Core.Service.Options
{
    public interface IOptionService
    {
        void ReLoad();
        void Set(string key, string value);
        string Get(string key, string defaultValue = null);
        void saveOption(string key, string value);
        IDictionary<string, string> getOptions();
        string getOption(string key);
        void deleteOption(string key);
    }
}