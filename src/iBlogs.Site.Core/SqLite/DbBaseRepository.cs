using System;
using System.Data.Common;
using iBlogs.Site.Core.Extensions;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

namespace iBlogs.Site.Core.SqLite
{
    public class DbBaseRepository : IDbBaseRepository
    {
        private readonly IConfiguration _configuration;
        public DbBaseRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string DbFile
        {
            get { return Environment.CurrentDirectory +"\\"+ _configuration[ConfigKey.SqLiteDbFileName].IfNullReturnDefaultValue("iBlogs.db"); }
        }

        public DbConnection DbConnection()
        {
            return new SqliteConnection("Data Source=" + DbFile);
        }
    }
}
