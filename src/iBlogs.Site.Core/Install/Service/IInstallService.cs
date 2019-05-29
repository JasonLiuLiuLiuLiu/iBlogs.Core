using System.Threading.Tasks;
using iBlogs.Site.Core.Install.DTO;

namespace iBlogs.Site.Core.Install.Service
{
    public interface IInstallService
    {
        Task<bool> InitializeDb(InstallParam installParam);
    }
}