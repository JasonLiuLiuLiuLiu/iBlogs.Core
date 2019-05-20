using System.Threading.Tasks;
using iBlogs.Site.Core.Entity;

namespace iBlogs.Site.Core.Service
{
    public interface IAttachService
    {
        Task<bool> Save(Attach attach);
    }
}