using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using iBlogs.Site.Application.Extensions;
using iBlogs.Site.Application.SqLite;

namespace iBlogs.Site.Application.Service.Options
{
    public class OptionService : IOptionService
    {
        private ISqLiteBaseRepository _sqLiteBaseRepository;
        private IDictionary<string, string> _options;

        public OptionService(ISqLiteBaseRepository sqLiteBaseRepository)
        {
            _sqLiteBaseRepository = sqLiteBaseRepository;
            _options = new Dictionary<string, string>();
        }

        public void ReLoad()
        {
            var options =
                _sqLiteBaseRepository.DbConnection().QueryAsync<Entity.Options>("select * from options");
            _options.Clear();
            foreach (var option in options.Result)
            {
                if (!_options.ContainsKey(option.Name))
                    _options.Add(option.Name, option.Value);
            }
        }

        public void Set(string key, string value)
        {
            key = key.Trim().ToLower();
            if (_options.ContainsKey(key))
                if (_options[key] == value)
                    return;
                else
                {
                    _sqLiteBaseRepository.DbConnection().Execute("update options set value=@value where name=@name",
                        new { value = value, name = key });
                    _options[key] = value;
                }
            else
            {
                _sqLiteBaseRepository.DbConnection().Execute("insert into options values(@name,@value)",
                    new { value = value, name = key });
                _options.Add(key, value);
            }
        }

        public string Get(string key, string defaultValue = null)
        {
            key = key.Trim().ToLower();
            if (_options.TryGetValue(key, out string value))
                return value;
            return defaultValue;
        }

        /**
        * 保存配置
        *
        * @param key   配置key
        * @param value 配置值
        */
        public void saveOption(String key, String value)
        {
            if (stringKit.isNotBlank(key) && stringKit.isNotBlank(value))
            {
                Set(key, value);
            }
        }

        /**
         * 获取系统配置
         */
        public IDictionary<string, string> getOptions()
        {
            return _options;
        }

        public String getOption(String key)
        {
            return Get(key);
        }

        /**
         * 根据key删除配置项
         *
         * @param key 配置key
         */
        public void deleteOption(string key)
        {
            if (_options.ContainsKey(key))
            {
                _options.Remove(key);
                _sqLiteBaseRepository.DbConnection().Execute("delete options  where name=@name", new { name = key });
            }
        }


    }
}
