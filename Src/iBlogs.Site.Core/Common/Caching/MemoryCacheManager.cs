using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;

namespace iBlogs.Site.Core.Common.Caching
{
    /// <summary>
    /// 
    /// </summary>
    public class MemoryCacheManager : ICacheManager
    {
        private readonly IMemoryCache _cache;

        protected CancellationTokenSource CANCELLATION_TOKEN_SOURCE;

        protected static readonly ConcurrentDictionary<string, bool> AllKeys;

        static MemoryCacheManager()
        {
            AllKeys = new ConcurrentDictionary<string, bool>();
        }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="cache"></param>
        public MemoryCacheManager(IMemoryCache cache)
        {
            _cache = cache;
            CANCELLATION_TOKEN_SOURCE = new CancellationTokenSource();
        }

        protected MemoryCacheEntryOptions GetMemoryCacheEntryOptions(int cacheTime)
        {
            var options = new MemoryCacheEntryOptions()
                .AddExpirationToken(new CancellationChangeToken(CANCELLATION_TOKEN_SOURCE.Token))
                .RegisterPostEvictionCallback(PostEviction);

            options.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(cacheTime);

            return options;
        }

        protected string AddKey(string key)
        {
            AllKeys.TryAdd(key, true);
            return key;
        }

        protected string RemoveKey(string key)
        {
            TryRemoveKey(key);
            return key;
        }

        protected void TryRemoveKey(string key)
        {
            if (!AllKeys.TryRemove(key, out bool _))
                AllKeys.TryUpdate(key, false, false);
        }

        private void ClearKeys()
        {
            foreach (var key in AllKeys.Where(p => !p.Value).Select(p => p.Key).ToList())
            {
                RemoveKey(key);
            }
        }

        private void PostEviction(object key, object value, EvictionReason reason, object state)
        {
            if (reason == EvictionReason.Replaced)
                return;

            ClearKeys();

            TryRemoveKey(key.ToString());
        }

        #region Methods

        /// <inheritdoc cref="ICacheManager"/>
        public T Get<T>(string key)
        {
            return _cache.Get<T>(key);
        }

        /// <inheritdoc cref="ICacheManager"/>
        public void Set(string key, object data, int cacheTime)
        {
            if (data != null)
            {
                _cache.Set(AddKey(key), data, GetMemoryCacheEntryOptions(cacheTime));
            }
        }

        /// <inheritdoc cref="ICacheManager"/>
        public bool IsSet(string key)
        {
            return _cache.TryGetValue(key, out object _);
        }

        /// <inheritdoc cref="ICacheManager"/>
        public void Remove(string key)
        {
            _cache.Remove(RemoveKey(key));
        }

        #endregion Methods
    }
}
