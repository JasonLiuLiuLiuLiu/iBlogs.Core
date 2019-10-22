using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace iBlogs.Site.GitAsDisk
{
    public class GitAsDiskService
    {
        private static bool _synced;
        private const string BasePath = "GitAsDisk";

        public static async Task<SyncResult> Sync(GitSyncOptions options)
        {
            using var syncImpl = new GitSyncImplement(BasePath, options.GitUrl, options.UserName, options.Password, options.BranchName, options.CommitterName);
            var syncResult = await syncImpl.Execute();
            _synced = syncResult.Result;
            return syncResult;

        }

        public static async Task<bool> CommitAsync<T>(IEnumerable<T> values) where T : class
        {
            if (!_synced)
            {
                throw new GitAsDiskException("Please Call Sync Method before this");
            }

            StringBuilder resultSb = new StringBuilder();
            foreach (var value in values)
            {
                resultSb.AppendLine(JsonConvert.SerializeObject(value).Replace("\r\n", " "));
            }

            return await CommitAsync(GetPathByType(typeof(T)), resultSb.ToString());
        }

        public static async Task<bool> CommitAsync(string fullPath, string value)
        {
            if (!_synced)
            {
                throw new GitAsDiskException("Please Call Sync Method before this");
            }

            var fileFullPath = Path.Combine(BasePath, fullPath);
            await File.WriteAllTextAsync(fileFullPath, value, Encoding.UTF8);

            return true;
        }

        public static async Task<string> LoadAsync(string fullPath)
        {
            if (!_synced)
            {
                throw new GitAsDiskException("Please Call Sync Method before this");
            }

            return await File.ReadAllTextAsync(Path.Combine(BasePath, fullPath), Encoding.UTF8);
        }

        public static async IAsyncEnumerable<T> LoadAsync<T>() where T : class
        {
            if (!_synced)
            {
                throw new GitAsDiskException("Please Call Sync Method before this");
            }

            var values = (await LoadAsync(GetPathByType(typeof(T)))).Split('\r', '\n');

            foreach (var value in values)
            {
                yield return JsonConvert.DeserializeObject<T>(value);
            }
        }

        private static string GetPathByType(Type T)
        {
            var typeFullName = T.FullName;

            Debug.Assert(typeFullName != null, nameof(typeFullName) + " != null");

            return typeFullName.Replace('.', '\\');
        }
    }
}
