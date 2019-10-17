namespace iBlogs.Site.GitAsDisk
{
    public class GitSyncOptions
    {
        public GitSyncOptions(string gitUrl, string uid, string pwd)
        {
            GitUrl = gitUrl;
            UserName = uid;
            Password = pwd;
        }

        public string BranchName { get; set; } = "Master";
        public string CommitterName { get; set; }
        public string GitUrl { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
