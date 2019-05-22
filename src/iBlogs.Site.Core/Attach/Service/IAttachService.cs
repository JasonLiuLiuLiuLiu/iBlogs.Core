using System.Threading.Tasks;

namespace iBlogs.Site.Core.Attach.Service
{
    public interface IAttachService
    {
        Task<bool> Save(Attachment attachment);
    }
}