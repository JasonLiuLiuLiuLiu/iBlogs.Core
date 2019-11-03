using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using iBlogs.Site.Core.Blog.Attach;
using iBlogs.Site.Core.Blog.Comment;
using iBlogs.Site.Core.Blog.Content;
using iBlogs.Site.Core.Blog.Extension;
using iBlogs.Site.Core.Blog.Meta;
using iBlogs.Site.Core.Blog.Relationship;
using iBlogs.Site.Core.Common;
using iBlogs.Site.Core.Common.Extensions;
using iBlogs.Site.Core.Option;
using iBlogs.Site.Core.Security;
using iBlogs.Site.Core.Storage;
using iBlogs.Site.GitAsDisk;
using Microsoft.Extensions.Configuration;

namespace iBlogs.Site.Core.Git
{
    public static class GitDataSyncHelper
    {
        private static readonly GitSyncOptions GitSyncOptions;
        private static readonly IConfiguration Configuration;

        static GitDataSyncHelper()
        {
            Configuration = ServiceFactory.GetService<IConfiguration>();
            if (Configuration != null)
                GitSyncOptions = new GitSyncOptions(Configuration["gitUrl"], Configuration["gitUid"], Configuration["gitPwd"]);
        }

        public static void DataSync()
        {
            DataSync(new CancellationToken());
        }

        public static void DataSync(object state)
        {
            Serilog.Log.Information("GitDataSync start...");

            var token = (CancellationToken)state;
            if (token.IsCancellationRequested)
                return;

            Pull();
            Push();

            Serilog.Log.Information("GitDataSync finished...");
        }

        private static void Pull()
        {
            try
            {
                var syncResult = GitAsDiskService.Pull(GitSyncOptions);
                if (!syncResult.Result)
                {
                    Serilog.Log.Error(syncResult.Message);
                    return;
                }

                if (Configuration["DataIsSynced"].ToBool())
                    return;

                var attachments = GitAsDiskService.Load<Attachment>();
                var blogSyncRelationships = GitAsDiskService.Load<BlogSyncRelationship>();
                var comments = GitAsDiskService.Load<Comments>();
                var contents = GitAsDiskService.Load<Contents>();
                var metas = GitAsDiskService.Load<Metas>();
                var options = GitAsDiskService.Load<Options>();
                var relationships = GitAsDiskService.Load<Relationships>();
                var users = GitAsDiskService.Load<Users>();

                StorageWarehouse.Set(ConvertToDic(attachments));
                StorageWarehouse.Set(ConvertToDic(blogSyncRelationships));
                StorageWarehouse.Set(ConvertToDic(comments));
                StorageWarehouse.Set(ConvertToDic(contents));
                StorageWarehouse.Set(ConvertToDic(metas));
                StorageWarehouse.Set(ConvertToDic(options));
                StorageWarehouse.Set(ConvertToDic(relationships));
                StorageWarehouse.Set(ConvertToDic(users));

                StorageWarehouse.BuildRelationShip();

                ConfigDataHelper.UpdateAppSettings("DataIsSynced", "true");
            }
            catch (Exception e)
            {
                Serilog.Log.Error(e, e.Message);
            }
        }

        private static void Push()
        {
            try
            {
                var attachments = StorageWarehouse.Get<Attachment>().Values.OrderBy(u => u.Id).ToList();
                var blogSyncRelationships = StorageWarehouse.Get<BlogSyncRelationship>().Values.OrderBy(u => u.Id).ToList();
                var comments = StorageWarehouse.Get<Comments>().Values.OrderBy(u => u.Id).ToList();
                var contents = StorageWarehouse.Get<Contents>().Values.OrderBy(u => u.Id).ToList();
                var metas = StorageWarehouse.Get<Metas>().Values.OrderBy(u => u.Id).ToList();
                var options = StorageWarehouse.Get<Options>().Values.OrderBy(u => u.Id).ToList();
                var relationships = StorageWarehouse.Get<Relationships>().Values.OrderBy(u => u.Id).ToList();
                var users = StorageWarehouse.Get<Users>().Values.OrderBy(u => u.Id).ToList();

                GitAsDiskService.Commit(attachments);
                GitAsDiskService.Commit(blogSyncRelationships);
                GitAsDiskService.Commit(contents);
                GitAsDiskService.Commit(comments);
                GitAsDiskService.Commit(metas);
                GitAsDiskService.Commit(options);
                GitAsDiskService.Commit(relationships);
                GitAsDiskService.Commit(users);

                var syncResult = GitAsDiskService.Push(GitSyncOptions);
                if (!syncResult.Result)
                {
                    Serilog.Log.Error(syncResult.Message);
                }
            }
            catch (Exception e)
            {
                Serilog.Log.Error(e, e.Message);
            }
        }

        private static ConcurrentDictionary<int, T> ConvertToDic<T>(IEnumerable<T> values) where T : IEntityBase
        {
            var result = new ConcurrentDictionary<int, T>();
            foreach (var value in values)
            {
                result.TryAdd(value.Id, value);
            }

            return result;
        }
    }
}
