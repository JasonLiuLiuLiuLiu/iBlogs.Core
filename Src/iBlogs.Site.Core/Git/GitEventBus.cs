using System;
using System.Collections.Generic;
using System.Text;
using DotNetCore.CAP;
using iBlogs.Site.Core.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

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
        public void Receive(GitRequest gitMessage)
        {
            _logger.LogInformation($"receive:{gitMessage.after}");
            _gitFileService.CloneOrPull();
        }
    }
}
