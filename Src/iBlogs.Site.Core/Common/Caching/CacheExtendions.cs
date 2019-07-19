using System;

namespace iBlogs.Site.Core.Common.Caching
{
    /// <summary>
    /// 缓存扩展方法
    /// </summary>
    public static class CacheExtensions
    {
        /// <summary>
        /// 默认缓存失效时间
        /// </summary>
        private static int DefaultCacheTimeMinutes => 60;

        /// <summary>
        /// 从缓存获取数据 如果不存在使用acquire生成并放入缓存
        /// 使用默认时效
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheManager"></param>
        /// <param name="key"></param>
        /// <param name="acquire"></param>
        /// <returns></returns>
        public static T Get<T>(this ICacheManager cacheManager, string key, Func<T> acquire)
        {
            return Get(cacheManager, key, DefaultCacheTimeMinutes, acquire);
        }

        /// <summary>
        /// 从缓存获取数据 如果不存在使用acquire生成并放入缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheManager"></param>
        /// <param name="key"></param>
        /// <param name="cacheTime"></param>
        /// <param name="acquire"></param>
        /// <returns></returns>
        public static T Get<T>(this ICacheManager cacheManager, string key, int cacheTime, Func<T> acquire)
        {
            if (cacheManager.IsSet(key))
                return cacheManager.Get<T>(key);

            var result = acquire();

            if (cacheTime > 0)
                cacheManager.Set(key, result, cacheTime);

            return result;
        }
    }
}
