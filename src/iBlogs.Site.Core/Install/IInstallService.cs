namespace iBlogs.Site.Core.Install
{
    public interface IInstallService
    {
        bool InitializeDb(string seedFileName = null);
    }
}