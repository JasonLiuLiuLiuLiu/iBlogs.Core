using iBlogs.Site.Core.Blog.Extension.Dto;

namespace iBlogs.Site.Core.Blog.Extension
{
    public interface IBlogSyncService
    {
        void Publish(BlogSyncRequest request);
    }
}