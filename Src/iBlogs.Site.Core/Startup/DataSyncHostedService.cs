using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace iBlogs.Site.Core.Startup
{
    public class DataSyncHostedService:IHostedService
    {
        private readonly ILogger<DataSyncHostedService> _logger;

        public DataSyncHostedService(ILogger<DataSyncHostedService> logger)
        {
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Data Sync Hosted Service running.");

            while (!cancellationToken.IsCancellationRequested)
            {
                //TODO
            }
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Data Sync Hosted Service is stopping.");
            return Task.CompletedTask;
        }
    }
}
