using System;
using System.Linq;
using DotNetCore.CAP;
using iBlogs.Site.Core.Blog.Content;
using iBlogs.Site.Core.Blog.Extension.Dto;
using iBlogs.Site.Core.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ArgumentOutOfRangeException = System.ArgumentOutOfRangeException;

namespace iBlogs.Site.Core.Blog.Extension
{
    public class BlogSyncService : IBlogSyncService
    {
        private readonly ICapPublisher _capPublisher;
        private readonly ILogger<BlogSyncService> _logger;
        private readonly IRepository<Contents> _contentRepository;

        public BlogSyncService(ICapPublisher capPublisher, ILogger<BlogSyncService> logger, IRepository<Contents> contentRepository)
        {
            _capPublisher = capPublisher;
            _logger = logger;
            _contentRepository = contentRepository;
        }

        public void Publish(BlogSyncRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var contexts = BuildContexts(request);

            foreach (var blogSyncContext in contexts)
            {
                _logger.LogInformation($"Publish Blog Sync Task,Cid:{request.Cid},Targets:{blogSyncContext.Target.ToString()}");
                switch (blogSyncContext.Target)
                {
                    case BlogSyncTarget.All:
                        _capPublisher.Publish("iBlogs.Site.Core.Blog.Sync.All", blogSyncContext);
                        break;
                    case BlogSyncTarget.CnBlogs:
                        _capPublisher.Publish("iBlogs.Site.Core.Blog.Sync.CnBlogs", blogSyncContext);
                        break;
                    case BlogSyncTarget.CSDN:
                        _capPublisher.Publish("iBlogs.Site.Core.Blog.Sync.CSDN", blogSyncContext);
                        break;
                    case BlogSyncTarget.WeChart:
                        _capPublisher.Publish("iBlogs.Site.Core.Blog.Sync.WeChart", blogSyncContext);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private BlogSyncContext[] BuildContexts(BlogSyncRequest request)
        {
            var content = _contentRepository.FirstOrDefault(request.Cid);

            if (content == null)
                throw new ArgumentException("传入的Cid有误");

            var postSyncDto = new PostSyncDto
            {
                Id = content.Id,
                Categories = content.Categories,
                Content = content.Content,
                Status = content.Status,
                Tags = content.Tags,
                Title = content.Title
            };

            return request.Targets.Select(t => new BlogSyncContext
            {
                Method = BlogSyncMethod.AddOrUpdate,
                Post = postSyncDto,
                Target = t
            }).ToArray();
        }
    }
}
