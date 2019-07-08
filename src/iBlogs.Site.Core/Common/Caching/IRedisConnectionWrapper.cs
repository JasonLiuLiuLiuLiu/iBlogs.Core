using StackExchange.Redis;

namespace iBlogs.Site.Core.Common.Caching
{
    /// <summary>
    /// Redis连接管理器
    /// </summary>
    public interface IRedisConnectionWrapper
    {
        /// <summary>
        /// 获取指定数据库
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        IDatabase GetDatabase(int? db = null);
    }
}
