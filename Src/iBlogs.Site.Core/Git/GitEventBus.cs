using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetCore.CAP;
using iBlogs.Site.Core.EntityFrameworkCore;
using iBlogs.Site.Core.Option;
using iBlogs.Site.Core.Option.Service;
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
        private readonly IOptionService _optionService;

        public GitEventBus(ICapPublisher capPublisher, ITransactionProvider transactionProvider, ILogger<GitEventBus> logger, IGitFileService gitFileService, IOptionService optionService)
        {
            _capPublisher = capPublisher;
            _transactionProvider = transactionProvider;
            _logger = logger;
            _gitFileService = gitFileService;
            _optionService = optionService;
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

                foreach (var commit in gitRequest.commits)
                {
                    if (commit.committer.name == _optionService.Get(ConfigKey.GitCommitter, "iBlogs"))
                    {
                        _logger.LogWarning($"commit:{commit.message},committer:{commit.committer.name} will skip");
                        continue;
                    }
                    needHandleFile.AddRange(commit.added);
                    needHandleFile.AddRange(commit.modified);
                }
                if (!needHandleFile.Any())
                    return;
                _gitFileService.CloneOrPull();
                await _gitFileService.Handle(needHandleFile.Distinct().ToList());
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.ToString());
                throw;
            }

        }
    }
}
