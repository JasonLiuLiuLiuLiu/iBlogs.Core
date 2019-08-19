using System;
using System.Linq;
using DotNetCore.CAP;
using iBlogs.Site.Core.Blog.Content;
using iBlogs.Site.Core.Blog.Extension.Dto;
using iBlogs.Site.Core.Common.Extensions;
using iBlogs.Site.Core.Common.Response;
using iBlogs.Site.Core.EntityFrameworkCore;
using iBlogs.Site.Core.Option;
using iBlogs.Site.Core.Option.Service;
using Microsoft.Extensions.Logging;
using ArgumentOutOfRangeException = System.ArgumentOutOfRangeException;

namespace iBlogs.Site.Core.Blog.Extension
{
    public class BlogSyncService : IBlogSyncService
    {
        private readonly ICapPublisher _capPublisher;
        private readonly ILogger<BlogSyncService> _logger;
        private readonly IRepository<Contents> _contentRepository;
        private readonly IOptionService _optionService;

        public BlogSyncService(ICapPublisher capPublisher, ILogger<BlogSyncService> logger, IRepository<Contents> contentRepository, IOptionService optionService)
        {
            _capPublisher = capPublisher;
            _logger = logger;
            _contentRepository = contentRepository;
            _optionService = optionService;
        }

        public ApiResponse Sync(BlogSyncRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (!Validation(request,out string errorMessage))
                return ApiResponse.Fail(errorMessage);

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

            return ApiResponse.Ok();
        }

        private bool Validation(BlogSyncRequest request,out string errorMessage)
        {
            errorMessage = null;
            if (request.Targets.Any(u => (u == BlogSyncTarget.All || u == BlogSyncTarget.CnBlogs))&&!ValidationForCnBlogs(out errorMessage))
               return false;
            return true;
        }

        private bool ValidationForCnBlogs(out string errorMessage)
        {
            errorMessage = null;
            var userName = _optionService.Get(ConfigKey.CnBlogsUserName);
            var passWord = _optionService.Get(ConfigKey.CnBlogsPassword);
            if (!userName.IsNullOrWhiteSpace() && !passWord.IsNullOrWhiteSpace()) return true;
            errorMessage = "同步数据到博客园前请先设置博客园用户名和密码";
            _logger.LogError(errorMessage);
            return false;
        }

        private BlogSyncContext[] BuildContexts(BlogSyncRequest request)
        {
            var postSyncDto = new PostSyncDto();
            if (request.Cid != 0)
            {
                var content = _contentRepository.FirstOrDefault(request.Cid);
                if (content == null)
                    throw new ArgumentException("传入的Cid有误");

                postSyncDto = new PostSyncDto
                {
                    Id = content.Id,
                    Categories = content.Categories,
                    Content = content.Content,
                    Status = content.Status,
                    Tags = content.Tags,
                    Title = content.Title
                };
            }

            return request.Targets.Select(t => new BlogSyncContext
            {
                Method = request.Method,
                Post = postSyncDto,
                Target = t
            }).ToArray();
        }
    }
}
