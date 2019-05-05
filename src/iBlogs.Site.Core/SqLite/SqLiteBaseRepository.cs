using iBlogs.Site.Common;
using iBlogs.Site.Common.Extensions;
using Microsoft.Data.Sqlite;
using System;
using iBlogs.Site.Common.Configuration;

namespace iBlogs.Site.Core.SqLite
{
    public class SqLiteBaseRepository
    {
        public static string DbFile
        {
            get { return Environment.CurrentDirectory + AppConfigurations.Get()[ConfigKey.SqLiteDbFileName].IfNullReturnDefaultValue("iBlogs.db"); }
        }

        public static SqliteConnection SimpleDbConnection()
        {
            return new SqliteConnection("Data Source=" + DbFile);
        }
    }
}
