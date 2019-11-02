using System;
using System.Threading;
using System.Threading.Tasks;
using iBlogs.Site.Core.Common;
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

        public ScheduleHostedService(ILogger<ScheduleHostedService> logger, IConfiguration configuration)
        {
            _logger = logger;
            var sleepTimeSpan = TimeSpan.FromMinutes(configuration?["AutoDataSyncMinutes"].ToIntOrDefault(10) ?? 10);
            BlogsTimer.Start(sleepTimeSpan);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Data Sync Hosted Service Running.");
            BlogsTimer.Register(() => GitDataSyncHelper.DataSync(cancellationToken));
            BlogsTimer.Register(() => LogClean(cancellationToken));
            return Task.CompletedTask;
        }



        private void LogClean(object state)
        {

        }



        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Data Sync Hosted Service is stopping.");
            BlogsTimer.Stop();
            return Task.CompletedTask;
        }

        public void Dispose()
        {
        }
    }
}
