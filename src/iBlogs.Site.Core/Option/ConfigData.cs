using System.Collections.Generic;

namespace iBlogs.Site.Core.Option
{
    public class ConfigData
    {
        private static readonly IDictionary<ConfigKey, string> Options = new Dictionary<ConfigKey, string>();

        static ConfigData()
        {
            InitData();
        }

        public static string Get(ConfigKey key, string defaultValue=null)
        {
            return Options.ContainsKey(key) ? Options[key] : defaultValue;
        }

        public static IDictionary<ConfigKey, string> GetAll()
        {
            var result=new Dictionary<ConfigKey,string>();
            foreach (var keyValuePair in Options)
            {
                result.Add(keyValuePair.Key,keyValuePair.Value);
            }
            return result;
        }

        internal static void Set(ConfigKey key, string value)
        {
            if (Options.ContainsKey(key))
                Options[key] = value;
            else
                Options.Add(key, value);
        }

        internal static void Set(ConfigKey key, int value)
        {
            Set(key, value.ToString());
        }

        private static void InitData()
        {
            Set(ConfigKey.MaxPage, 100);
            Set(ConfigKey.MaxTextCount, 200000);
            Set(ConfigKey.MaxTitleCount, 200);
            Set(ConfigKey.MaxIntroCount, 500);
            Set(ConfigKey.MaxFileSize, 204800);
            Set(ConfigKey.StaticUrl, "/static");
            Set(ConfigKey.TemplesPath, "/templates/");
            Set(ConfigKey.ThemePath, "themes/default");
        }
    }
}
