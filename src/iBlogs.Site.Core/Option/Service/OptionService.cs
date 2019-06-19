﻿using System;
using System.Collections.Generic;
using System.Linq;
using iBlogs.Site.Core.Common.Extensions;
using iBlogs.Site.Core.EntityFrameworkCore;

namespace iBlogs.Site.Core.Option.Service
{
    public class OptionService 
    {
        private readonly IRepository<Options> _repository;
        private static IDictionary<ConfigKey, string> _options = new Dictionary<ConfigKey, string>();

        public OptionService(IRepository<Options> repository)
        {
            _repository = repository;
            TryLoad();
        }

        private void TryLoad()
        {
            try
            {
                if (_options.Count == 0)
                    _options = _repository.GetAll().ToDictionary(o => (ConfigKey)Enum.Parse(typeof(ConfigKey),o.Name), o => o.Value);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }

        public void Set(ConfigKey key, string value, string description = null)
        {
            if (_options.ContainsKey(key))
                if (_options[key] == value)
                    return;
                else
                {
                    var entity = _repository.GetAll().FirstOrDefault(o => o.Name == key);
                    if (entity != null)
                    {
                        entity.Value = value;
                        if (!description.IsNullOrWhiteSpace())
                            entity.Description = description;
                        _repository.Update(entity);
                        _repository.SaveChanges();
                    }

                    _options[key] = value;
                }
            else
            {
                _repository.Insert(new Options { Name = key, Value = value, Description = description });
                _options.Add(key, value);
                _repository.SaveChanges();
            }
        }

        public string Get(ConfigKey key, string defaultValue = null)
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
        public void saveOption(ConfigKey key, string value, string description = null)
        {
            if (stringKit.isNotBlank(key) && stringKit.isNotBlank(value))
            {
                Set(key, value, description);
            }
        }

        /**
         * 获取系统配置
         */
        public IDictionary<string, string> getOptions()
        {
            return _options;
        }

        public string getOption(ConfigKey key)
        {
            return Get(key);
        }

        /**
         * 根据key删除配置项
         *
         * @param key 配置key
         */
        public void deleteOption(ConfigKey key)
        {
            if (_options.ContainsKey(key))
            {
                _options.Remove(key);
                _repository.Delete(_repository.GetAll().FirstOrDefault(o => o.Name == key));
            }
        }

        public IDictionary<string, string> GetAll()
        {
            return _options;
        }

        public void SaveOptions(IDictionary<string, string> options)
        {
            if (options == null)
                return;
            foreach (var option in options)
            {
                saveOption(option.Key, option.Value);
            }
        }

    }
}
