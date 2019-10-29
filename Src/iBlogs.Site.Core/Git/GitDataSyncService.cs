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
using Microsoft.Extensions.Logging;

namespace iBlogs.Site.Core.Git
{
    public class GitDataSyncService : IGitDataSyncService
    {
        private readonly GitSyncOptions _gitSyncOptions;
        private readonly IConfiguration _configuration;
        private readonly ILogger<GitDataSyncService> _logger;

        public GitDataSyncService(IConfiguration configuration,  ILogger<GitDataSyncService> logger)
        {
            _configuration = configuration;
            _logger = logger;
            if (configuration != null)
                _gitSyncOptions = new GitSyncOptions(configuration["gitUrl"], configuration["gitUid"], configuration["gitPwd"]);
        }

        public void DataSync(object state)
        {
            var token = (CancellationToken)state;
            if (token.IsCancellationRequested)
                return;

            Pull();
            Push();
        }

        private void Pull()
        {
            try
            {
                var syncResult = GitAsDiskService.Sync(_gitSyncOptions);
                if (!syncResult.Result)
                {
                    _logger.LogError(syncResult.Message);
                    return;
                }

                if (_configuration["DataIsSynced"].ToBool())
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
                _logger.LogError(e, e.Message);
            }
        }

        private void Push()
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

                var syncResult = GitAsDiskService.Sync(_gitSyncOptions);
                if (!syncResult.Result)
                {
                    _logger.LogError(syncResult.Message);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
            }
        }

        private ConcurrentDictionary<int, T> ConvertToDic<T>(IEnumerable<T> values) where T : IEntityBase
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
