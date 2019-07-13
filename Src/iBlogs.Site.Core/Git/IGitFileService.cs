namespace iBlogs.Site.Core.Git
{
    public interface IGitFileService
    {
        bool CloneOrPull();
    }
}