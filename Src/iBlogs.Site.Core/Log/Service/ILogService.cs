using iBlogs.Site.Core.Common.Request;
using iBlogs.Site.Core.Common.Response;
using iBlogs.Site.Core.Log.Dto;

namespace iBlogs.Site.Core.Log.Service
{
    public interface ILogService
    {
        Page<LogResponse> GetPage(PageParam page);
    }
}