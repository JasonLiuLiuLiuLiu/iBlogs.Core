using System;
using System.Threading;
using System.Threading.Tasks;
using iBlogs.Site.Core.Common.Extensions;
using iBlogs.Site.GitAsDisk;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace iBlogs.Site.Core.Startup
{
    public class DataSyncHostedService : IHostedService
    {
        private readonly ILogger<DataSyncHostedService> _logger;
        private readonly TimeSpan _sleepTimeSpan;

        public DataSyncHostedService(ILogger<DataSyncHostedService> logger, IConfiguration configuration)
        {
            _logger = logger;
            _sleepTimeSpan = TimeSpan.FromHours(configuration?["AutoDataSyncHours"].ToIntOrDefault(1) ?? 1);
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Data Sync Hosted Service Running.");

            while (!cancellationToken.IsCancellationRequested)
            {
                //TODO read git sync options from config
                await GitAsDiskService.Sync(new GitSyncOptions("", "", "")).ConfigureAwait(false);
                Thread.Sleep(_sleepTimeSpan);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Data Sync Hosted Service is stopping.");
            return Task.CompletedTask;
        }
    }
}
