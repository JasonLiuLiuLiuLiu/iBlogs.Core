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

        public GitSyncOptions(string gitUrl, string uid, string pwd, string path) : this(gitUrl, uid, pwd)
        {
            Path = path;
        }

        public string Path { get; set; } = "GitAsDisk";
        public string GitUrl { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
