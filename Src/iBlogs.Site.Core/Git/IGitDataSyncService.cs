using System.Threading;

namespace iBlogs.Site.Core.Git
{
    public interface IGitDataSyncService
    {
        void DataSync(object token);
    }
}