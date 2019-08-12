using iBlogs.Site.Core.Blog.Extension.Dto;
using iBlogs.Site.Core.Common.Response;

namespace iBlogs.Site.Core.Blog.Extension
{
    public interface IBlogSyncService
    {
        ApiResponse Sync(BlogSyncRequest request);
    }
}