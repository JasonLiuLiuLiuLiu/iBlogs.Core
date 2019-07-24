using System;
using System.Linq;
using System.Threading.Tasks;
using iBlogs.Site.Core.Common.Extensions;
using iBlogs.Site.Core.EntityFrameworkCore;
using iBlogs.Site.Core.Option;
using iBlogs.Site.Core.Option.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace iBlogs.Site.Core.Blog.Extension.CnBlogs
{
    public class CnBlogsSyncExtension : IBlogsSyncExtension
    {
        private readonly IOptionService _optionService;
        private readonly IRepository<BlogSyncRelationship> _repository;
        private readonly ILogger<CnBlogsSyncExtension> _logger;

        public CnBlogsSyncExtension(IOptionService optionService, IRepository<BlogSyncRelationship> repository, ILogger<CnBlogsSyncExtension> logger)
        {
            _optionService = optionService;
            _repository = repository;
            _logger = logger;
        }

        public async Task Sync(BlogSyncContext context)
        {
            if (!_optionService.Get(ConfigKey.CnBlogsSyncSwitch, "false").ToBool())
                return;
            var userName = _optionService.Get(ConfigKey.CnBlogsUserName);
            var passWord = _optionService.Get(ConfigKey.CnBlogsPassword);
            if (userName.IsNullOrWhiteSpace() ||passWord.IsNullOrWhiteSpace())
            {
                var errorMessage = "同步数据到博客园前请先设置博客园用户名和密码";
                _logger.LogError(errorMessage);
                context.Success = false;
                context.Message = errorMessage;
                return;
            }

            switch (context.Method)
            {
                case BlogSyncMethod.AddOrUpdate:
                    await AddOrUpdate(context);
                    break;
                case BlogSyncMethod.Delete:
                    await Delete(context);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public async Task InitializeSync()
        {
            await Task.CompletedTask;
        }

        private async Task AddOrUpdate(BlogSyncContext context)
        {
            var relationship = await _repository.GetAll().Where(u => u.Target == BlogSyncTarget.CnBlogs)
                  .FirstOrDefaultAsync(u => u.ContentId == context.Post.Id);
            if (relationship == null)
            {
                await Add(context);
            }
            else
            {
                await Update(context, relationship);
            }
        }

        private async Task Add(BlogSyncContext context)
        {
            throw new NotImplementedException();
        }

        private async Task Update(BlogSyncContext context, BlogSyncRelationship relationship)
        {
            throw new NotImplementedException();
        }

        private async Task Delete(BlogSyncContext context)
        {
            throw new NotImplementedException();
        }
    }
}
