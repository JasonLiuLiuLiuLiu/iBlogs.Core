using System;
using System.Collections.Generic;
using System.Linq;
using iBlogs.Site.Core.Common.Extensions;
using iBlogs.Site.Core.Option.DTO;
using iBlogs.Site.Core.Storage;

namespace iBlogs.Site.Core.Option.Service
{
    public class OptionService : IOptionService
    {
        private readonly IRepository<Options> _repository;
        private static bool _init;
        private static readonly object InitLock = new object();

        public OptionService(IRepository<Options> repository)
        {
            _repository = repository;
            if (_init) return;
            lock (InitLock)
            {
                if (_init)
                    return;
                Load();
                _init = true;
            }
        }

        public void Load()
        {
            CheckConfigKeyAttribute();
        }

        public IDictionary<ConfigKey, string> GetAllAsKeyValue()
        {
            return _repository.GetAll().ToDictionary(o => (ConfigKey)Enum.Parse(typeof(ConfigKey), o.Name), o =>
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
            }).OrderBy(u => u.Key).ToList();
        }

        public List<OptionParam> GetEditable()
        {
            return _repository.GetAll().Where(u => u.Editable).Select(u => new OptionParam
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
        }

        public string Get(ConfigKey key, string defaultValue = null)
        {
            var optionEntity = _repository.GetAll().FirstOrDefault(o => o.Name == key.ToString());
            if (optionEntity == null || optionEntity.Value.IsNullOrWhiteSpace()) return defaultValue;
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
                    _repository.Insert(new Options { Name = attribute.Key, Value = attribute.Value, Description = attribute.Description, Editable = attribute.Editable });
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
                    var entity = _repository.GetAll().FirstOrDefault(o => o.Name == exist.Key);
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
        }
    }
}