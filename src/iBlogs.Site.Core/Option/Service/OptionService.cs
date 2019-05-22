using System.Collections.Generic;
using iBlogs.Site.Core.Common.Extensions;
using iBlogs.Site.Core.SqLite;

namespace iBlogs.Site.Core.Option.Service
{
    public class OptionService : IOptionService
    {
        private IDbBaseRepository _dbBaseRepository;
        private IDictionary<string, string> _options;

        public OptionService(IDbBaseRepository dbBaseRepository)
        {
            _dbBaseRepository = dbBaseRepository;
            _options = new Dictionary<string, string>();
            ReLoad();
        }

        public void ReLoad()
        {
            var options =
                _dbBaseRepository.DbConnection().QueryAsync<Options>("select * from t_options");
            _options.Clear();
            foreach (var option in options.Result)
            {
                if (!_options.ContainsKey(option.Name))
                    _options.Add(option.Name, option.Value);
            }
        }

        public void Set(string key, string value)
        {
            if (_options.ContainsKey(key))
                if (_options[key] == value)
                    return;
                else
                {
                    _dbBaseRepository.DbConnection().Execute("update t_options set value=@value where name=@name",
                        new { value = value, name = key });
                    _options[key] = value;
                }
            else
            {
                _dbBaseRepository.DbConnection().Execute("insert into t_options (name,value) values(@name,@value)",
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
        public void saveOption(string key, string value)
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

        public string getOption(string key)
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
                _dbBaseRepository.DbConnection().Execute("delete options  where name=@name", new { name = key });
            }
        }


    }
}
