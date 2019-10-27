using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using iBlogs.Site.Core.Blog.Attach;
using iBlogs.Site.Core.Blog.Comment;
using iBlogs.Site.Core.Blog.Content;
using iBlogs.Site.Core.Blog.Extension;
using iBlogs.Site.Core.Blog.Meta;
using iBlogs.Site.Core.Blog.Relationship;
using iBlogs.Site.Core.Common.Extensions;
using iBlogs.Site.Core.Option;
using iBlogs.Site.Core.Security;
using iBlogs.Site.Core.Storage;
using iBlogs.Site.GitAsDisk;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace iBlogs.Site.Core.Startup
{
    public sealed class DataSyncHostedService : IHostedService, IDisposable
    {
        private readonly ILogger<DataSyncHostedService> _logger;
        private Timer _timer;
        private readonly TimeSpan _sleepTimeSpan;
        private CancellationToken _token;
        private readonly GitSyncOptions _gitSyncOptions;

        public DataSyncHostedService(ILogger<DataSyncHostedService> logger, IConfiguration configuration)
        {
            _logger = logger;
            _sleepTimeSpan = TimeSpan.FromHours(configuration?["AutoDataSyncHours"].ToIntOrDefault(1) ?? 1);
            if (configuration != null)
                _gitSyncOptions = new GitSyncOptions(configuration["gitUrl"], configuration["gitUid"], configuration["gitPwd"]);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _token = cancellationToken;

            _logger.LogInformation("Data Sync Hosted Service Running.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero, _sleepTimeSpan);

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            if (_token.IsCancellationRequested)
                return;

            try
            {
                GitAsDiskService.Sync(_gitSyncOptions);
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

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Data Sync Hosted Service is stopping.");
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer.Dispose();
        }
    }
}
