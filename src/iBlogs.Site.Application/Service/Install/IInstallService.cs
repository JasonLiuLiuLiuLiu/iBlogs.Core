namespace iBlogs.Site.Application.Service.Install
{
    public interface IInstallService
    {
        bool InitializeDb(string seedFileName = null);
    }
}