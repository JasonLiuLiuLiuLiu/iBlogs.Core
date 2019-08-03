using System;
using System.Collections.Generic;
using System.Linq;
using iBlogs.Site.Core.Common.Caching;
using iBlogs.Site.Core.Common.Extensions;
using iBlogs.Site.Core.EntityFrameworkCore;
using iBlogs.Site.Core.Option.DTO;
using Microsoft.EntityFrameworkCore;

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

            CheckConfigKeyAttribute();

            foreach (var keyValuePair in GetAllAsKeyValue())
            {
                _cacheManager.Set(keyValuePair.Key.ToCacheKey(), keyValuePair.Value, _defaultCacheTime);
            }
        }

        public IDictionary<ConfigKey, string> GetAllAsKeyValue()
        {
            return _repository.GetAll().AsNoTracking().ToDictionary(o => (ConfigKey)Enum.Parse(typeof(ConfigKey), o.Name), o =>
            {
                if (o.Value != null)
                    return o.Value;
                return "";
            });
        }

        public List<OptionParam> GetAll()
        {
            return _repository.GetAll().Select(u => new OptionParam
            {
                Id = u.Id,
                Key = u.Name,
                Value = u.Value,
                Description = u.Description
            }).OrderBy(u=>u.Key).ToList();
        }

        public List<OptionParam> GetEditable()
        {
            return _repository.GetAll().Where(u=>u.Editable).Select(u => new OptionParam
            {
                Id = u.Id,
                Key = u.Name,
                Value = u.Value,
                Description = u.Description
            }).OrderBy(u => u.Key).ToList();
        }

        public void Set(ConfigKey key, string value, string description = null)
        {
            var entity = _repository.GetAll().FirstOrDefault(o => o.Name == key.ToString());
            if (entity != null)
            {
                entity.Value = value;
                if (!description.IsNullOrWhiteSpace())
                    entity.Description = description;
                _repository.Update(entity);
            }
            else
            {
                _repository.Insert(new Options { Name = key.ToString(), Value = value, Description = description });
            }
            _repository.SaveChanges();
            _cacheManager.Set(key.ToCacheKey(), value, _defaultCacheTime);
        }

        public string Get(ConfigKey key, string defaultValue = null)
        {
            var result = _cacheManager.Get<string>(key.ToCacheKey());
            if (!result.IsNullOrWhiteSpace())
                return result;

            var optionEntity = _repository.GetAll().FirstOrDefault(o => o.Name == key.ToString());
            if (optionEntity == null || optionEntity.Value.IsNullOrWhiteSpace()) return defaultValue;
            _cacheManager.Set(key.ToCacheKey(), optionEntity.Value, _defaultCacheTime);
            return optionEntity.Value;
        }
        private void CheckConfigKeyAttribute()
        {
            var allAttributes = ConfigKeyAttribute.GetConfigKeyAttribute();

            if (allAttributes == null)
                return;

            var existAll = GetAll();

            foreach (var attribute in allAttributes)
            {
                var exist = existAll.FirstOrDefault(u => u.Key == attribute.Key);
                var update = false;
                if (exist == null)
                {
                    _repository.Insert(new Options { Name = attribute.Key, Value = attribute.Value, Description = attribute.Description,Editable = attribute.Editable});
                    continue;
                }

                if (exist.Value.IsNullOrWhiteSpace() && !attribute.Value.IsNullOrWhiteSpace())
                {
                    exist.Value = attribute.Value;
                    update = true;
                }



                if (exist.Description.IsNullOrWhiteSpace() && !attribute.Description.IsNullOrWhiteSpace())
                {
                    exist.Description = attribute.Description;
                    update = true;
                }

                if (exist.Editable != attribute.Editable)
                {
                    exist.Editable = attribute.Editable;
                    update = true;
                }

                if (update)
                {
                    var entity = _repository.GetAll().FirstOrDefault(o => o.Name == exist.Key.ToString());
                    if (entity != null)
                    {
                        entity.Value = exist.Value;
                        if (!exist.Description.IsNullOrWhiteSpace())
                            entity.Description = exist.Description;
                        entity.Editable = exist.Editable;
                        _repository.Update(entity);
                    }
                }
            }
            _repository.SaveChanges();
        }
    }
}