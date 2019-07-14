using System;
using System.Collections.Generic;
using System.Linq;
using iBlogs.Site.Core.Common.Caching;
using iBlogs.Site.Core.Common.Extensions;
using iBlogs.Site.Core.EntityFrameworkCore;
using iBlogs.Site.Core.Option.DTO;

namespace iBlogs.Site.Core.Option.Service
{
    public class OptionService : IOptionService
    {
        private readonly IRepository<Options> _repository;
        private readonly ICacheManager _cacheManager;
        private readonly int _defaultCacheTime = (int)new TimeSpan(1, 0, 0, 0).TotalMilliseconds;

        public OptionService(IRepository<Options> repository, ICacheManager cacheManager)
        {
            _repository = repository;
            _cacheManager = cacheManager;
        }

        public void Load()
        {
            ConfigData.Init(this);

            foreach (var keyValuePair in GetAllAsKeyValue())
            {
                _cacheManager.Set(keyValuePair.Key.ToCacheKey(), keyValuePair.Value, _defaultCacheTime);
            }
        }

        public IDictionary<ConfigKey, string> GetAllAsKeyValue()
        {
            return _repository.GetAll().ToDictionary(o => (ConfigKey)Enum.Parse(typeof(ConfigKey), o.Name), o => o.Value);
        }

        public List<OptionParam> GetAll()
        {
            return _repository.GetAll().Select(u => new OptionParam
            {
                Id = u.Id,
                Key = u.Name,
                Value = u.Value,
                Description = u.Description
            }).ToList();
        }

        public void Set(ConfigKey key, string value, string description = null)
        {
            if (_cacheManager.Get<string>(key.ToCacheKey()) == value) return;

            if (_cacheManager.Get<string>(key.ToCacheKey()) == null)
            {
                _repository.Insert(new Options { Name = key.ToString(), Value = value, Description = description });
                _repository.SaveChanges();
            }
            else
            {
                var entity = _repository.GetAll().FirstOrDefault(o => o.Name == key.ToString());
                if (entity != null)
                {
                    entity.Value = value;
                    if (!description.IsNullOrWhiteSpace())
                        entity.Description = description;
                    _repository.Update(entity);
                    _repository.SaveChanges();
                }
            }
            _cacheManager.Set(key.ToCacheKey(), value, _defaultCacheTime);
        }

        public string Get(ConfigKey key, string defaultValue = null)
        {
            var result = _cacheManager.Get<string>(key.ToCacheKey());
            if (result != null)
                return result;

            var optionEntity = _repository.GetAll().FirstOrDefault(o => o.Name == key.ToString());
            if (optionEntity == null) return defaultValue;
            _cacheManager.Set(key.ToCacheKey(), optionEntity.Value, _defaultCacheTime);
            return optionEntity.Value;

        }
    }
}