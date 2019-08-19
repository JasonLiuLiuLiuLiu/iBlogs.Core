using System;
using System.Linq;
using iBlogs.Site.Core.Option.DTO;

namespace iBlogs.Site.Core.Option
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class ConfigKeyAttribute : Attribute
    {
        public string Description { get; set; }
        public string DefaultValue { get; set; }
        public bool Editable { get; set; }

        public ConfigKeyAttribute(string description) : this(description, false)
        {
        }

        public ConfigKeyAttribute(string description, bool editable) : this(description, null, editable)
        {
        }

        public ConfigKeyAttribute(string description, string defaultValue) : this(description, defaultValue, false)
        {
        }

        public ConfigKeyAttribute(string description, string defaultValue, bool editable)
        {
            DefaultValue = defaultValue;
            Description = description;
            Editable = editable;
        }

        public static OptionParam[] GetConfigKeyAttribute()
        {
            var fields = typeof(ConfigKey).GetFields().Where(u => u.FieldType.BaseType == typeof(Enum)).ToArray();
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
                        Description = attribute.Description,
                        Editable = attribute.Editable
                    };
                }
            }
            return result;
        }
    }
}
