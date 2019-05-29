using System.Collections.Generic;

namespace iBlogs.Site.Core.Option.Service
{
    public interface IOptionService
    {
        void TryReLoad();
        void Set(string key, string value, string description = null);
        string Get(string key, string defaultValue = null);
        void saveOption(string key, string value, string description = null);
        IDictionary<string, string> getOptions();
        string getOption(string key);
        void deleteOption(string key);
        IDictionary<string, string> GetAll();
        void SaveOptions(IDictionary<string, string> options);
    }
}