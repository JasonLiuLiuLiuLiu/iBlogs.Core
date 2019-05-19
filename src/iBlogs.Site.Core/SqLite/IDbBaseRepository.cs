using System.Data.Common;

namespace iBlogs.Site.Core.SqLite
{
    public interface IDbBaseRepository
    {
        string DbFile { get; }
        DbConnection DbConnection();
    }
}