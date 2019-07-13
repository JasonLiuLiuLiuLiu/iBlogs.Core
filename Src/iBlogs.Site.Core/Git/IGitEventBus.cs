namespace iBlogs.Site.Core.Git
{
    public interface IGitEventBus
    {
        bool Publish(string message);
        void Receive(GitRequest gitMessage);
    }
}