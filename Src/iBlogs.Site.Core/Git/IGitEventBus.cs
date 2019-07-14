using System.Threading.Tasks;

namespace iBlogs.Site.Core.Git
{
    public interface IGitEventBus
    {
        bool Publish(string message);
        Task Receive(string message);
    }
}