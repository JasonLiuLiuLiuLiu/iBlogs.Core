using System.Threading.Tasks;
using iBlogs.Site.Core.Common.Request;
using iBlogs.Site.Core.Common.Response;

namespace iBlogs.Site.Core.Attach.Service
{
    public interface IAttachService
    {
        Task<bool> Save(Attachment attachment);
        Page<Attachment> GetPage(PageParam param);
        void Delete(int id);
        int GetTotalCount();
    }
}