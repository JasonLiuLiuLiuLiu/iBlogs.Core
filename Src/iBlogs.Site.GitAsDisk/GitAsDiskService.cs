using System;
using System.Threading.Tasks;

namespace iBlogs.Site.GitAsDisk
{
    public class GitAsDiskService : IGitAsDiskService
    {
        public Task<bool> Sync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Commit<T>(T value)
        {
            throw new NotImplementedException();
        }

        public Task<T> Load<T>()
        {
            throw new NotImplementedException();
        }
    }
}
