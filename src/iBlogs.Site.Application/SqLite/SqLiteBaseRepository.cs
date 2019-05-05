using iBlogs.Site.Common;
using iBlogs.Site.Common.Extensions;
using Microsoft.Data.Sqlite;
using System;
using Microsoft.Extensions.Configuration;

namespace iBlogs.Site.Core.SqLite
{
    public class SqLiteBaseRepository : ISqLiteBaseRepository
    {
        private readonly IConfiguration _configuration;
        public SqLiteBaseRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string DbFile
        {
            get { return Environment.CurrentDirectory + _configuration[ConfigKey.SqLiteDbFileName].IfNullReturnDefaultValue("iBlogs.db"); }
        }

        public SqliteConnection DbConnection()
        {
            return new SqliteConnection("Data Source=" + DbFile);
        }
    }
}
