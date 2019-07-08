namespace iBlogs.Site.Core.Common.Caching
{
    /// <summary>
    /// 缓存访问接口
    /// </summary>
    public interface ICacheManager
    {
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        T Get<T>(string key);

        /// <summary>
        /// 存入数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <param name="cacheTime"></param>
        void Set(string key, object data, int cacheTime);

        /// <summary>
        /// 判断key是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool IsSet(string key);

        /// <summary>
        /// 移除数据
        /// </summary>
        /// <param name="key"></param>
        void Remove(string key);
    }
}
