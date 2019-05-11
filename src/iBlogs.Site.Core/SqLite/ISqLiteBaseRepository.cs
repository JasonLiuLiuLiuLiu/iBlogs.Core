using Microsoft.Data.Sqlite;

namespace iBlogs.Site.Core.SqLite
{
    public interface ISqLiteBaseRepository
    {
        string DbFile { get; }
        SqliteConnection DbConnection();
    }
}