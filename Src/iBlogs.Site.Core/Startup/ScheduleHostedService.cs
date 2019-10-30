using System;
using System.Threading;
using System.Threading.Tasks;
using iBlogs.Site.Core.Common.Extensions;
using iBlogs.Site.Core.Git;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace iBlogs.Site.Core.Startup
{
    public sealed class ScheduleHostedService : IHostedService, IDisposable
    {
        private readonly ILogger<ScheduleHostedService> _logger;
        private Timer _dataSyncTimer;
        private Timer _logClearTimer;
        private readonly TimeSpan _sleepTimeSpan;
        private readonly IGitDataSyncService _gitDataSyncService;

        public ScheduleHostedService(ILogger<ScheduleHostedService> logger, IConfiguration configuration, IGitDataSyncService gitDataSyncService)
        {
            _logger = logger;
            _gitDataSyncService = gitDataSyncService;
            _sleepTimeSpan = TimeSpan.FromHours(configuration?["AutoDataSyncHours"].ToIntOrDefault(1) ?? 1);

        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Data Sync Hosted Service Running.");

            _dataSyncTimer = new Timer(_gitDataSyncService.DataSync, cancellationToken, TimeSpan.Zero, _sleepTimeSpan);
            _logClearTimer = new Timer(LogClean, null, TimeSpan.Zero, _sleepTimeSpan);

            return Task.CompletedTask;
        }



        private void LogClean(object state)
        {

        }



        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Data Sync Hosted Service is stopping.");
            _dataSyncTimer?.Change(Timeout.Infinite, 0);
            _logClearTimer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _dataSyncTimer.Dispose();
            _logClearTimer.Dispose();
        }
    }
}
