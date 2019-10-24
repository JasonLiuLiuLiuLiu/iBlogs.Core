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
    public sealed class DataSyncHostedService : IHostedService, IDisposable
    {
        private readonly ILogger<DataSyncHostedService> _logger;
        private Timer _timer;
        private readonly TimeSpan _sleepTimeSpan;
        private CancellationToken _token;

        public DataSyncHostedService(ILogger<DataSyncHostedService> logger, IConfiguration configuration)
        {
            _logger = logger;
            _sleepTimeSpan = TimeSpan.FromHours(configuration?["AutoDataSyncHours"].ToIntOrDefault(1) ?? 1);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _token = cancellationToken;

            _logger.LogInformation("Data Sync Hosted Service Running.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero, _sleepTimeSpan);

            return Task.CompletedTask;
        }

        private async void DoWork(object state)
        {
            if(_token.IsCancellationRequested)
                return;

            //TODO read git sync options from config
            await GitAsDiskService.Sync(new GitSyncOptions("", "", "")).ConfigureAwait(false);
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
