using System.Data;
using Dapper;

namespace iBlogs.Site.Core.Utils.Extensions
{
    public static class DapperExtension
    {
        public static int QueryCount(this IDbConnection con, string sql, object param = null)
        {
            return con.QueryFirstOrDefault<int>("SELECT COUNT(1) FROM (" + sql + ")", param);
        }
    }
}
