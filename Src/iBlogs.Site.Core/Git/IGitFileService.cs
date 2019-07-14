using System.Collections.Generic;
using System.Threading.Tasks;

namespace iBlogs.Site.Core.Git
{
    public interface IGitFileService
    {
        bool CloneOrPull();
        Task<bool> Handle(List<string> files);
        bool CommitAndPush();
    }
}