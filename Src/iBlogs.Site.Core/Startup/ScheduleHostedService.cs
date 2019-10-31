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

        public ScheduleHostedService(ILogger<ScheduleHostedService> logger, IConfiguration configuration)
        {
            _logger = logger;
            _sleepTimeSpan = TimeSpan.FromMinutes(configuration?["AutoDataSyncMinutes"].ToIntOrDefault(10) ?? 10);

        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Data Sync Hosted Service Running.");

            _dataSyncTimer = new Timer(GitDataSyncHelper.DataSync, cancellationToken, TimeSpan.Zero, _sleepTimeSpan);
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
