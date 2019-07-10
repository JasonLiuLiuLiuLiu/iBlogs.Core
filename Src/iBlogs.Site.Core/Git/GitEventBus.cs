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

        public GitEventBus(ICapPublisher capPublisher, ITransactionProvider transactionProvider, ILogger<GitEventBus> logger)
        {
            _capPublisher = capPublisher;
            _transactionProvider = transactionProvider;
            _logger = logger;
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
        public void Receive(string message)
        {
            _logger.LogInformation($"receive:{message}");
        }
    }
}
