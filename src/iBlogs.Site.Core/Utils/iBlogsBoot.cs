using System;
using System.IO;
using iBlogs.Site.Core.Dto;
using iBlogs.Site.Core.Entity;
using iBlogs.Site.Core.Extensions;
using iBlogs.Site.Core.Utils.Attribute;
using Microsoft.Extensions.Configuration;

namespace iBlogs.Site.Core.Utils
{
    public class IBlogsBoot
    {
        public static void Startup(IConfiguration configuration)
        {
            CheckDbInstallStatus(configuration);
            SetDapperMapping();
        }
        private static void CheckDbInstallStatus(IConfiguration configuration)
        {
            if (File.Exists(Environment.CurrentDirectory + "\\" + configuration[ConfigKey.SqLiteDbFileName].IfNullReturnDefaultValue("iBlogs.db")))
                ConfigDataHelper.UpdateDbInstallStatus(true);
            else
                ConfigDataHelper.UpdateDbInstallStatus(false);
        }

        private static void SetDapperMapping()
        {
            Dapper.SqlMapper.SetTypeMap(typeof(Comment),new ColumnAttributeTypeMapper<Comments>());
            Dapper.SqlMapper.SetTypeMap(typeof(Contents),new ColumnAttributeTypeMapper<Contents>());
            Dapper.SqlMapper.SetTypeMap(typeof(Logs),new ColumnAttributeTypeMapper<Logs>());
            Dapper.SqlMapper.SetTypeMap(typeof(Metas),new ColumnAttributeTypeMapper<Metas>());
            Dapper.SqlMapper.SetTypeMap(typeof(Options),new ColumnAttributeTypeMapper<Options>());
            Dapper.SqlMapper.SetTypeMap(typeof(Relationships),new ColumnAttributeTypeMapper<Relationships>());
            Dapper.SqlMapper.SetTypeMap(typeof(Users),new ColumnAttributeTypeMapper<Users>());
        }
    }
}
