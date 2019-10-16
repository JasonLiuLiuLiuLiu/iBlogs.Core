using System.Threading.Tasks;

namespace iBlogs.Site.GitAsDisk
{
    public interface IGitAsDiskService
    {
        Task<bool> Sync();
        Task<bool> Commit<T>(T value);
        Task<T> Load<T>();
    }
}
