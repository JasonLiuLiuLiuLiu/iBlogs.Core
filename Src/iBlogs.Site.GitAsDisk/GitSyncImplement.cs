using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

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

        public GitSyncImplement(string path, string gitUrl, string userName, string password, string branchName, string committerName)
        {
            _path = path;
            _gitUrl = gitUrl;
            _userName = userName;
            _password = password;
            _branchName = branchName;
            _committerName = committerName;
        }

        public async Task<SyncResult> Execute()
        {
            try
            {
                if (Directory.Exists(_path))
                {
                    await Clone();
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

        private async Task<bool> Clone()
        {
            throw new NotImplementedException();
        }
        private async Task<bool> CheckOut()
        {
            throw new NotImplementedException();
        }
        private async Task<bool> Pull()
        {
            throw new NotImplementedException();
        }

        private async Task<bool> CommitAll()
        {
            throw new NotImplementedException();
        }

        private async Task<bool> Push()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
