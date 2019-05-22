namespace iBlogs.Site.Core.Install.Service
{
    public interface IInstallService
    {
        bool InitializeDb(string seedFileName = null);
    }
}