using System;
using System.IO;
using iBlogs.Site.Core.Comment;
using iBlogs.Site.Core.Comment.DTO;
using iBlogs.Site.Core.Common.Attribute;
using iBlogs.Site.Core.Common.Extensions;
using iBlogs.Site.Core.Content;
using iBlogs.Site.Core.Log;
using iBlogs.Site.Core.Meta;
using iBlogs.Site.Core.Option;
using iBlogs.Site.Core.Relationship;
using iBlogs.Site.Core.User;
using Microsoft.Extensions.Configuration;

namespace iBlogs.Site.Core.Common
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
            Dapper.SqlMapper.SetTypeMap(typeof(CommentDto),new ColumnAttributeTypeMapper<Comments>());
            Dapper.SqlMapper.SetTypeMap(typeof(Contents),new ColumnAttributeTypeMapper<Contents>());
            Dapper.SqlMapper.SetTypeMap(typeof(Logs),new ColumnAttributeTypeMapper<Logs>());
            Dapper.SqlMapper.SetTypeMap(typeof(Metas),new ColumnAttributeTypeMapper<Metas>());
            Dapper.SqlMapper.SetTypeMap(typeof(Options),new ColumnAttributeTypeMapper<Options>());
            Dapper.SqlMapper.SetTypeMap(typeof(Relationships),new ColumnAttributeTypeMapper<Relationships>());
            Dapper.SqlMapper.SetTypeMap(typeof(Users),new ColumnAttributeTypeMapper<Users>());
        }
    }
}
