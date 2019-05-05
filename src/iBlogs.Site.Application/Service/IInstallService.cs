namespace iBlogs.Site.Application.Service
{
    public interface IInstallService
    {
        void InitializeDbAsync(string seedFileName = null);
    }
}