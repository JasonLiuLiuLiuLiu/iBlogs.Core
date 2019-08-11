using System.Threading.Tasks;
using iBlogs.Site.Core.Blog.Extension.Dto;

namespace iBlogs.Site.Core.Blog.Extension
{
    public interface IBlogsSyncExtension
    {
         Task Sync(BlogSyncContext context);
         Task InitializeSync();
         BlogExtensionDashboardResponse GetDashBoardData();
    }
}