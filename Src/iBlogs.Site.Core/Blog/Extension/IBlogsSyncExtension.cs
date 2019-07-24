using System.Threading.Tasks;

namespace iBlogs.Site.Core.Blog.Extension
{
    public interface IBlogsSyncExtension
    {
         Task Sync(BlogSyncContext context);
         Task InitializeSync();
    }
}