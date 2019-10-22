using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LibGit2Sharp;

namespace iBlogs.Site.GitAsDisk
{
    internal class GitSyncImplement : IDisposable
    {
        private readonly string _path;
        private readonly string _gitUrl;
        private readonly string _userName;
        private readonly string _password;
        private readonly string _branchName;
        private readonly string _committerName;
        private readonly string _committerEmail;
        private readonly Repository _repo;

        private CancellationToken _token;
        private int _retryCount;

        public GitSyncImplement(string path, string gitUrl, string userName, string password, string branchName, string committerName, string committerEmail)
        {
            _path = path;
            _gitUrl = gitUrl;
            _userName = userName;
            _password = password;
            _branchName = branchName;
            if (string.IsNullOrEmpty(_branchName))
            {
                _branchName = "master";
            }
            _committerName = committerName;
            _committerEmail = committerEmail;
            _repo = new LibGit2Sharp.Repository(_path);
        }

        public async Task<SyncResult> Execute(CancellationToken token)
        {
            _token = token;
            _retryCount = 0;
            try
            {
                if (!Directory.EnumerateFileSystemEntries(_path).Any())
                {
                    await CloneAsync();
                    await CheckOut();
                }
                else
                {
                    await CheckOut();
                    await Pull();
                }

                await CommitAll();
                await Push();
            }
            catch (Exception e)
            {
                return SyncResult.Failed(e.ToString());
            }

            return SyncResult.Success();

        }

        private async Task<bool> CloneAsync()
        {
            await Task.Factory.StartNew(() =>
            {
                if (!Directory.Exists(_path))
                    Directory.CreateDirectory(_path);
                try
                {
                    if (!Directory.EnumerateFileSystemEntries(_path).Any())
                    {
                        var co = new CloneOptions
                        {
                            CredentialsProvider = (url, user, cred) => new UsernamePasswordCredentials
                            {
                                Username = _userName,
                                Password = _password
                            },
                            BranchName = _branchName
                        };
                        Repository.Clone(_gitUrl, _path, co);
                    }
                    else
                    {
                        await Pull();
                    }
                }
                catch
                {
                    Directory.Delete(_path, true);
                    Directory.CreateDirectory(_path);
                    var co = new CloneOptions
                    {
                        CredentialsProvider = (url, user, cred) => new UsernamePasswordCredentials
                        {
                            Username = _userName,
                            Password = _password
                        }
                    };
                    Repository.Clone(_gitUrl, _path, co);
                }
            }, _token);
           

            return true;
        }
        private async Task<bool> CheckOut()
        {
            await Task.Factory.StartNew(() =>
            {
                var trackingBranch = _repo.Branches["remotes/origin/" + _branchName];
                if (trackingBranch == null || !trackingBranch.IsRemote) return;
                var branch = _repo.Branches[_branchName];
                if (branch == null)
                {
                    branch = _repo.CreateBranch(_branchName, trackingBranch.Tip);
                }

                _repo.Branches.Update(branch, b => b.TrackedBranch = trackingBranch.CanonicalName);

                Commands.Checkout(_repo, branch, new CheckoutOptions { CheckoutModifiers = CheckoutModifiers.Force });
            }, _token);

            return true;
        }
        private async Task<bool> Pull()
        {
            await Task.Factory.StartNew(() =>
            {
                // Credential information to fetch
                PullOptions options = new PullOptions
                {
                    FetchOptions = new FetchOptions
                    {
                        CredentialsProvider = (url, usernameFromUrl, types) =>
                            new UsernamePasswordCredentials()
                            {
                                Username = _userName,
                                Password = _password
                            }
                    }
                };

                // User information to create a merge commit
                var signature = new Signature(
                    new Identity("MERGE_USER_NAME", "MERGE_USER_EMAIL"), DateTimeOffset.Now);

                // Pull
                Commands.Pull(_repo, signature, options);
            }, _token);

            return true;
        }

        private async Task<bool> CommitAll()
        {
            await Task.Factory.StartNew(() =>
            {
                Commands.Stage(_repo, "*");

                // Create the committer's signature and commit
                var author = new Signature(_committerName, _committerEmail, DateTime.Now);
                var committer = author;

                // Commit to the repository
                _repo.Commit($"GitAsDisk Commit at {DateTime.Now.ToString(CultureInfo.InvariantCulture)}", author, committer);
            }, _token);

            return true;
        }

        private async Task<bool> Push()
        {
            await Task.Factory.StartNew(() =>
            {
                var options = new PushOptions
                {
                    CredentialsProvider = (url, usernameFromUrl, types) =>
                        new UsernamePasswordCredentials()
                        {
                            Username = _userName,
                            Password = _password
                        }
                };
                _repo.Network.Push(_repo.Branches[_branchName], options);
            }, _token);

            return true;
        }

        public void Dispose()
        {
            _repo.Dispose();
        }
    }
}
