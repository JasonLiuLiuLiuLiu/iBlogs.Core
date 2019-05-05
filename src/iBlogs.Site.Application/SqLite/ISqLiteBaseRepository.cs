using Microsoft.Data.Sqlite;

namespace iBlogs.Site.Application.SqLite
{
    public interface ISqLiteBaseRepository
    {
        string DbFile { get; }
        SqliteConnection DbConnection();
    }
}