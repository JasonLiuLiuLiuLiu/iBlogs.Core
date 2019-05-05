using iBlogs.Site.Common.Extensions;
using iBlogs.Site.Core.Configuration;
using Microsoft.Data.Sqlite;
using System;

namespace iBlogs.Site.Core.SqLite
{
    public class SqLiteBaseRepository
    {
        public static string DbFile
        {
            get { return Environment.CurrentDirectory + AppConfigurations.Get()[ConfigKey.SqliteDbFileName].IfNullReturnDefaultValue("iBlogs.db"); }
        }

        public static SqliteConnection SimpleDbConnection()
        {
            return new SqliteConnection("Data Source=" + DbFile);
        }
    }
}
