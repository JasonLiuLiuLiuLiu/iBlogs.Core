namespace iBlogs.Site.Application.Service
{
    public interface IInstallService
    {
        bool InitializeDb(string seedFileName = null);
    }
}