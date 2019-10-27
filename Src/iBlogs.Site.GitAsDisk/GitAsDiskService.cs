using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace iBlogs.Site.GitAsDisk
{
    public class GitAsDiskService
    {
        private static bool _synced;
        private const string BasePath = "GitAsDisk";

        public static SyncResult Sync(GitSyncOptions options)
        {
            using var syncImpl = new GitSyncImplement(BasePath, options.GitUrl, options.UserName, options.Password, options.BranchName, options.CommitterName, options.CommitterEmail);
            var syncResult = syncImpl.Execute(options.Token);
            _synced = syncResult.Result;
            return syncResult;
        }

        public static bool Commit<T>(IEnumerable<T> values) where T : class
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

            return Commit(GetPathByType(typeof(T)), resultSb.ToString());
        }

        public static bool Commit(string fullPath, string value)
        {
            if (!_synced)
            {
                throw new GitAsDiskException("Please Call Sync Method before this");
            }

            var fileFullPath = Path.Combine(BasePath, fullPath);
            File.WriteAllText(fileFullPath, value, Encoding.UTF8);

            return true;
        }

        public static string Load(string fullPath)
        {
            if (!_synced)
            {
                throw new GitAsDiskException("Please Call Sync Method before this");
            }

            return File.ReadAllText(Path.Combine(BasePath, fullPath), Encoding.UTF8);
        }

        public static IEnumerable<T> Load<T>() where T : class
        {
            if (!_synced)
            {
                throw new GitAsDiskException("Please Call Sync Method before this");
            }

            var values = (Load(GetPathByType(typeof(T)))).Split('\r', '\n');

            foreach (var value in values)
            {
                if (string.IsNullOrEmpty(value)) continue;
                var result = JsonConvert.DeserializeObject<T>(value);
                if (result == null) continue;
                yield return result;
            }
        }

        private static string GetPathByType(Type T)
        {
            var typeFullName = T.FullName;

            Debug.Assert(typeFullName != null, nameof(typeFullName) + " != null");

            var path = typeFullName.Replace('.', '\\');

            var directory = Path.GetDirectoryName(path);
            var fullDirectory = Path.Combine(BasePath, directory ?? "");
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(fullDirectory))
            {
                Directory.CreateDirectory(fullDirectory);
            }
            return path;
        }
    }
}
