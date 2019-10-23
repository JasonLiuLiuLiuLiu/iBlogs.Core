using System;
using System.Threading.Tasks;
using Dapper;
using iBlogs.Site.Core.Blog.Attach;
using iBlogs.Site.Core.Blog.Comment;
using iBlogs.Site.Core.Blog.Content;
using iBlogs.Site.Core.Blog.Extension;
using iBlogs.Site.Core.Blog.Meta;
using iBlogs.Site.Core.Blog.Relationship;
using iBlogs.Site.Core.Option;
using iBlogs.Site.Core.Security;
using MySql.Data.MySqlClient;
using NUnit.Framework;

namespace iBlogs.Site.GitAsDisk.Test
{
    [Ignore("Generator")]
    public class DataFilesGenerator
    {
        private GitSyncOptions _syncOptions;
        [SetUp]
        public void Setup()
        {
            var gitUrl = Environment.GetEnvironmentVariable("gitUrl");
            var gitUid = Environment.GetEnvironmentVariable("gitUid");
            var gitPwd = Environment.GetEnvironmentVariable("gitPwd");
            _syncOptions = new GitSyncOptions(gitUrl, gitUid, gitPwd);
        }

        [Test]
        public async Task GenerateFromMysql()
        {
            var sql = new string[]
            {
                "select * from attachments",
                "select * from blogsyncrelationships",
                "select * from comments",
                "select * from contents",
                "select * from metas",
                "select * from options",
                "select * from relationships",
                "select * from users",
            };
            await using var con = new MySqlConnection("Server=localhost;Database=iblogs;uid=root;pwd=123456");
            var attachments = con.Query<Attachment>(sql[0]);
            var blogsyncrelationships = con.Query<BlogSyncRelationship>(sql[1]);
            var comments = con.Query<Comments>(sql[2]);
            var contents = con.Query<Contents>(sql[3]);
            var metas = con.Query<Metas>(sql[4]);
            var options = con.Query<Options>(sql[5]);
            var relationships = con.Query<Relationships>(sql[6]);
            var users = con.Query<Users>(sql[7]);

            await GitAsDiskService.Sync(_syncOptions);

            await GitAsDiskService.CommitAsync(attachments);
            await GitAsDiskService.CommitAsync(blogsyncrelationships);
            await GitAsDiskService.CommitAsync(comments);
            await GitAsDiskService.CommitAsync(contents);
            await GitAsDiskService.CommitAsync(metas);
            await GitAsDiskService.CommitAsync(options);
            await GitAsDiskService.CommitAsync(relationships);
            await GitAsDiskService.CommitAsync(users);

            await GitAsDiskService.Sync(_syncOptions);

            Assert.Pass();
        }
    }
}