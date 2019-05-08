namespace iBlogs.Site.Core.Service.Install
{
    public interface IInstallService
    {
        bool InitializeDb(string seedFileName = null);
    }
}