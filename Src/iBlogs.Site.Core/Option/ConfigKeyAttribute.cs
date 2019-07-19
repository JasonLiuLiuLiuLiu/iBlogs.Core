using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iBlogs.Site.Core.Option.DTO;

namespace iBlogs.Site.Core.Option
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class ConfigKeyAttribute : Attribute
    {
        public string Description { get; set; }
        public string DefaultValue { get; set; }

        public ConfigKeyAttribute(string description, string defaultValue = null)
        {
            DefaultValue = defaultValue;
            Description = description;
        }

        public static OptionParam[] GetConfigKeyAttribute()
        {
            var fields = typeof(ConfigKey).GetFields().Where(u=>u.FieldType.BaseType==typeof(Enum)).ToArray();
            var result = new OptionParam[fields.Length];
            for (int i = 0; i < fields.Length; i++)
            {
                var attribute = (ConfigKeyAttribute)fields[i].GetCustomAttributes(typeof(ConfigKeyAttribute), false).FirstOrDefault();
                if (attribute == null)
                {
                    result[i] = new OptionParam
                    {
                        Key = fields[i].Name
                    };
                }
                else
                {
                    result[i] = new OptionParam
                    {
                        Key = fields[i].Name,
                        Value = attribute.DefaultValue,
                        Description = attribute.Description
                    };
                }
            }
            return result;
        }
    }
}
