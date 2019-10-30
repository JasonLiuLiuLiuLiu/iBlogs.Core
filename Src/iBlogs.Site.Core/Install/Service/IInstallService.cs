using iBlogs.Site.Core.Install.DTO;
using System.Threading.Tasks;

namespace iBlogs.Site.Core.Install.Service
{
    public interface IInstallService
    {
        bool InitializeDb();

        void WriteInstallInfo(InstallParam param);
    }
}