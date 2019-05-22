using System.Data;

namespace iBlogs.Site.Core.Common.Extensions
{
    public static class DapperExtension
    {
        public static int QueryCount(this IDbConnection con, string sql, object param = null)
        {
            return con.QueryFirstOrDefault<int>("SELECT COUNT(1) FROM (" + sql + ")", param);
        }
    }
}
