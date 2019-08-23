using System.Collections.Generic;
using System.Threading.Tasks;

namespace iBlogs.Site.Core.Git
{
    public interface IGitFileService
    {
        bool CloneOrPull(string branchName);
        Task<bool> Handle(List<string> files, string branchName);
        bool CommitAndPush(string branchName);
    }
}