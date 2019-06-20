using System;
using System.Linq;
using iBlogs.Site.Core.Common.Extensions;
using iBlogs.Site.Core.EntityFrameworkCore;

namespace iBlogs.Site.Core.Option.Service
{
    public class OptionService : IOptionService
    {
        private readonly IRepository<Options> _repository;

        public OptionService(IRepository<Options> repository)
        {
            _repository = repository;
        }

        public void Load()
        {
            foreach (var keyValuePair in _repository.GetAll().ToDictionary(o => (ConfigKey)Enum.Parse(typeof(ConfigKey), o.Name), o => o.Value))
            {
                ConfigData.Set(keyValuePair.Key, keyValuePair.Value);
            }
        }

        public void Set(ConfigKey key, string value, string description = null)
        {
            if (ConfigData.Get(key) == value) return;

            if (ConfigData.Get(key) == null)
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
            ConfigData.Set(key, value);
        }

        public string Get(ConfigKey key, string defaultValue = null)
        {
            return ConfigData.Get(key, defaultValue);
        }
    }
}
