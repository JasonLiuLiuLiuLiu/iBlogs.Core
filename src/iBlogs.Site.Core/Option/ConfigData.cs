using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace iBlogs.Site.Core.Option
{
    public class ConfigData
    {
        private static readonly IDictionary<ConfigKey, string> Options = new Dictionary<ConfigKey, string>();

        static ConfigData()
        {

        }

        public string this[ConfigKey key]
        {
            get => Options.ContainsKey(key) ? Options[key] : null;
            set
            {
                if (Options.ContainsKey(key))
                    Options[key] = value;
                else
                    Options.Add(key, value);
            }
        }
    }
}
