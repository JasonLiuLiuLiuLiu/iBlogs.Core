using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetCore.CAP;
using iBlogs.Site.Core.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace iBlogs.Site.Core.Git
{
    public class GitEventBus : ICapSubscribe, IGitEventBus
    {
        private readonly ICapPublisher _capPublisher;
        private readonly ITransactionProvider _transactionProvider;
        private readonly ILogger<GitEventBus> _logger;
        private readonly IGitFileService _gitFileService;

        public GitEventBus(ICapPublisher capPublisher, ITransactionProvider transactionProvider, ILogger<GitEventBus> logger, IGitFileService gitFileService)
        {
            _capPublisher = capPublisher;
            _transactionProvider = transactionProvider;
            _logger = logger;
            _gitFileService = gitFileService;
        }

        public bool Publish(string message)
        {
            using (_transactionProvider.CreateTransaction())
            {
                _logger.LogInformation($"send:{message}");
                _capPublisher.Publish("iBlogs.Site.Core.GitEventBus", message);
                return true;
            }
        }

        [CapSubscribe("iBlogs.Site.Core.GitEventBus")]
        public async Task Receive(string message)
        {
            _logger.LogInformation($"receive:{message}");
            try
            {
                var gitRequest = JsonConvert.DeserializeObject<GitRequest>(message);
                if (gitRequest?.commits == null)
                    return;
                var needHandleFile = new List<string>();

                needHandleFile.AddRange(gitRequest.commits.SelectMany(u => u.added));
                needHandleFile.AddRange(gitRequest.commits.SelectMany(u => u.modified));

                if (!needHandleFile.Any())
                    return;
                _gitFileService.CloneOrPull();
                await _gitFileService.Handle(needHandleFile);
            }
            catch(Exception ex)
            {
                _logger.LogInformation(ex.ToString());
                throw;
            }

        }
    }
}
