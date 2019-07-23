using System;
using System.Linq;
using System.Threading.Tasks;
using iBlogs.Site.Core.Common.Extensions;
using iBlogs.Site.Core.EntityFrameworkCore;
using iBlogs.Site.Core.Option;
using iBlogs.Site.Core.Option.Service;
using Microsoft.EntityFrameworkCore;

namespace iBlogs.Site.Core.Blog.Extension.CnBlogs
{
    public class CnBlogsSyncExtension : IBlogsSyncExtension
    {
        private readonly IOptionService _optionService;
        private readonly IRepository<BlogAsyncRelationship> _repository;

        public CnBlogsSyncExtension(IOptionService optionService, IRepository<BlogAsyncRelationship> repository)
        {
            _optionService = optionService;
            _repository = repository;
        }

        public async Task Sync(BlogAsyncContext context)
        {
            if (!_optionService.Get(ConfigKey.CnBlogsSyncSwitch, "false").ToBool())
                return;

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

        public async Task InitializeAsync()
        {
            await Task.CompletedTask;
        }

        private async Task AddOrUpdate(BlogAsyncContext context)
        {
            var relationship = await _repository.GetAll().Where(u => u.Target == AsyncTarget.CnBlogs)
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

        private async Task Add(BlogAsyncContext context)
        {
            throw new NotImplementedException();
        }

        private async Task Update(BlogAsyncContext context, BlogAsyncRelationship relationship)
        {
            throw new NotImplementedException();
        }

        private async Task Delete(BlogAsyncContext context)
        {
            throw new NotImplementedException();
        }
    }
}
