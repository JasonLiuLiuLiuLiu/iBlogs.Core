
using System;
using System.IO;
using System.Linq;
using iBlogs.Site.Core.Option;
using iBlogs.Site.Core.Option.Service;
using LibGit2Sharp;

namespace iBlogs.Site.Core.Git
{
    public class GitFileService : IGitFileService
    {
        private readonly IOptionService _optionService;
        private const string RepoPath = "Repo";
        public GitFileService(IOptionService optionService)
        {
            _optionService = optionService;
        }


        public bool CloneOrPull()
        {
            if (!Directory.Exists(RepoPath))
                Directory.CreateDirectory(RepoPath);
            if (!Directory.EnumerateFileSystemEntries(RepoPath).Any())
            {
                var co = new CloneOptions();
                co.CredentialsProvider = (url, user, cred) => new UsernamePasswordCredentials { Username = _optionService.Get(ConfigKey.GitUerName), Password = _optionService.Get(ConfigKey.GitPassword) };
                LibGit2Sharp.Repository.Clone(_optionService.Get(ConfigKey.GitProjectCloneUrl), RepoPath, co);
            }
            else
            {
                using (var repo = new LibGit2Sharp.Repository(RepoPath))
                {
                    // Credential information to fetch
                    PullOptions options = new PullOptions();
                    options.FetchOptions = new FetchOptions();
                    options.FetchOptions.CredentialsProvider = (url, usernameFromUrl, types) =>
                        new UsernamePasswordCredentials()
                        {
                            Username = _optionService.Get(ConfigKey.GitUerName),
                            Password = _optionService.Get(ConfigKey.GitPassword)
                        };

                    // User information to create a merge commit
                    var signature = new Signature(
                        new Identity("MERGE_USER_NAME", "MERGE_USER_EMAIL"), DateTimeOffset.Now);

                    // Pull
                    Commands.Pull(repo, signature, options);
                }
            }


            return true;
        }
    }
}
