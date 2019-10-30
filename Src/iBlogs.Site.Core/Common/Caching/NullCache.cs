namespace iBlogs.Site.Core.Common.Caching
{
    /// <summary>
    /// 无缓存
    /// </summary>
    public class NopNullCache : ICacheManager
    {
        /// <inheritdoc cref="ICacheManager"/>
        public T Get<T>(string key)
        {
            return default(T);
        }

        /// <inheritdoc cref="ICacheManager"/>
        public void Set(string key, object data, int cacheTime)
        {
        }

        /// <inheritdoc cref="ICacheManager"/>
        public bool IsSet(string key)
        {
            return false;
        }

        /// <inheritdoc cref="ICacheManager"/>
        public void Remove(string key)
        {
        }

        /// <inheritdoc cref="ICacheManager"/>
        public void RemoveByPattern(string pattern)
        {
        }

        /// <inheritdoc cref="ICacheManager"/>
        public void Clear()
        {
        }

        /// <inheritdoc cref="ICacheManager"/>
        public void Dispose()
        {
        }
    }
}
