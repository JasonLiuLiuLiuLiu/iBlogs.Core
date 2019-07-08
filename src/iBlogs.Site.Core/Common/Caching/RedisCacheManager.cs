using System;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace iBlogs.Site.Core.Common.Caching
{
    /// <summary>
    /// 
    /// </summary>
    public class RedisCacheManager : ICacheManager
    {
        private readonly IRedisConnectionWrapper _connectionWrapper;
        private readonly IDatabase _db;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="connectionWrapper"></param>
        public RedisCacheManager(IRedisConnectionWrapper connectionWrapper)
        {
            _connectionWrapper = connectionWrapper;

            _db = _connectionWrapper.GetDatabase();
        }


        protected async Task<T> GetAsync<T>(string key)
        {
            var serializedItem = await _db.StringGetAsync(key);
            if (!serializedItem.HasValue)
                return default(T);

            var item = JsonConvert.DeserializeObject<T>(serializedItem);
            if (item == null)
                return default(T);

            return item;
        }

        protected async Task SetAsync(string key, object data, int cacheTime)
        {
            if (data == null)
                return;

            var expiresIn = TimeSpan.FromMinutes(cacheTime);

            var serializedItem = JsonConvert.SerializeObject(data);

            await _db.StringSetAsync(key, serializedItem, expiresIn);
        }

        protected async Task<bool> IsSetAsync(string key)
        {
            return await _db.KeyExistsAsync(key);
        }

        protected async Task RemoveAsync(string key)
        {
            await _db.KeyDeleteAsync(key);
        }

        #region Methods

        /// <inheritdoc cref="ICacheManager"/>
        public T Get<T>(string key)
        {
            return GetAsync<T>(key).Result;
        }

        /// <inheritdoc cref="ICacheManager"/>
        public async void Set(string key, object data, int cacheTime)
        {
            await SetAsync(key, data, cacheTime);
        }

        /// <inheritdoc cref="ICacheManager"/>
        public bool IsSet(string key)
        {
            return IsSetAsync(key).Result;
        }

        /// <inheritdoc cref="ICacheManager"/>
        public async void Remove(string key)
        {
            await RemoveAsync(key);
        }

        #endregion
    }
}
