using System.Threading;

namespace iBlogs.Site.GitAsDisk
{
    public class GitSyncOptions
    {
        public GitSyncOptions(string gitUrl, string uid, string pwd)
        {
            GitUrl = gitUrl;
            UserName = uid;
            Password = pwd;
            BranchName = "master";
            CommitterName = "GitAsDisk";
            CommitterEmail = "admin@iblogs.site";
            Token = new CancellationToken();
        }

        public GitSyncOptions(string gitUrl, string uid, string pwd,string branchName,string committerName,string committerEmail,CancellationToken token)
        {
            GitUrl = gitUrl;
            UserName = uid;
            Password = pwd;
            BranchName = branchName;
            CommitterName = committerName;
            CommitterEmail = committerEmail;
            Token = token;
        }

        public string BranchName { get; set; }
        public string CommitterName { get; set; }
        public string CommitterEmail { get; set; }
        public string GitUrl { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public CancellationToken Token { get; set; }
    }
}
