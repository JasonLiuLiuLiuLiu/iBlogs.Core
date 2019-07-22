using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace iBlogs.Site.Core.Blog.Extension
{
    public class CnBlogsSyncExtension : IBlogsSyncExtension
    {

        public async Task Sync(BlogAsyncContext context)
        {
            switch (context.Method)
            {
                case BlogSyncMethod.Add:
                    await Add(context);
                    break;
                case BlogSyncMethod.Update:
                   await Update(context);
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
            //TODO 数据库初始化
        }

        private async Task Add(BlogAsyncContext context)
        {
            throw new NotImplementedException();
        }

        private async Task Update(BlogAsyncContext context)
        {
            throw new NotImplementedException();
        }

        private async Task Delete(BlogAsyncContext context)
        {
            throw new NotImplementedException();
        }
    }
}
