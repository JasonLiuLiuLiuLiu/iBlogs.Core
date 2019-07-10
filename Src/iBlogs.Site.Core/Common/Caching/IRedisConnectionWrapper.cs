using System.Threading.Tasks;
using StackExchange.Redis;

namespace iBlogs.Site.Core.Common.Caching
{
    /// <summary>
    /// Redis连接管理器
    /// </summary>
    public interface IRedisConnectionWrapper
    {
        Task<bool> CheckConnection(int timeOut = 10000);
        /// <summary>
        /// 获取指定数据库
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        IDatabase GetDatabase(int? db = null);
    }
}
