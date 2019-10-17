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
        private static bool _initiated;
        private static string _basePath;

        public static Task<bool> Sync(GitSyncOptions options)
        {
            _initiated = true;
            _basePath = options.Path;
            throw new NotImplementedException();
        }

        public static async Task<bool> CommitAsync<T>(IEnumerable<T> values) where T : class
        {
            if (!_initiated)
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
            if (!_initiated)
            {
                throw new GitAsDiskException("Please Call Sync Method before this");
            }

            var fileFullPath = Path.Combine(_basePath, fullPath);
            await File.WriteAllTextAsync(fileFullPath, value, Encoding.UTF8);

            return true;
        }

        public static async Task<string> LoadAsync(string fullPath)
        {
            if (!_initiated)
            {
                throw new GitAsDiskException("Please Call Sync Method before this");
            }

            return await File.ReadAllTextAsync(Path.Combine(_basePath, fullPath), Encoding.UTF8);
        }

        public static async IAsyncEnumerable<T> LoadAsync<T>() where T : class
        {
            if (!_initiated)
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
