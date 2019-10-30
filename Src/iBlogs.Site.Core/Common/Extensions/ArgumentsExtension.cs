using System;
using System.Collections.Generic;

namespace iBlogs.Site.Core.Common.Extensions
{
    public static class ArgumentsExtension
    {
        private const string BuildNumber = "BuildNumber";
        private const string GitUrl = "GitUrl";
        private const string GitUid = "GitUid";
        private const string GitPwd = "GitPwd";
        private static readonly Dictionary<string, string> Data = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        public static Dictionary<string, string> SetConfigInfo(this string[] args)
        {
            if (args == null || args.Length == 0)
                return Data;

            Load(args);

            if (!Data.ContainsKey(GitUrl) || !Data.ContainsKey(GitUid) || !Data.ContainsKey(GitPwd))
                return Data;

            ConfigDataHelper.UpdateAppSettings(GitUrl, Data[GitUrl]);
            ConfigDataHelper.UpdateAppSettings(GitUid, Data[GitUid]);
            ConfigDataHelper.UpdateAppSettings(GitPwd, Data[GitPwd]);
            ConfigDataHelper.UpdateDbInstallStatus(true);
            Console.WriteLine("Set connection string from command line.");

            if (Data.ContainsKey(BuildNumber))
                ConfigDataHelper.UpdateBuildNumber(Data[BuildNumber]);

            return Data;
        }

        private static void Load(IEnumerable<string> args)
        {
            using var enumerator = args.GetEnumerator();
            while (enumerator.MoveNext())
            {
                string key1 = enumerator.Current ?? "";
                int startIndex = 0;
                if (key1.StartsWith("--"))
                    startIndex = 2;
                else if (key1.StartsWith("-"))
                    startIndex = 1;
                else if (key1.StartsWith("/"))
                {
                    key1 = $"--{(object)key1.Substring(1)}";
                    startIndex = 2;
                }
                int length = key1.IndexOf('=');
                string index;
                string str;
                if (length < 0)
                {
                    if (startIndex != 0)
                    {
                        if (startIndex != 1)
                            index = key1.Substring(startIndex);
                        else
                            continue;
                        if (enumerator.MoveNext())
                            str = enumerator.Current;
                        else
                            continue;
                    }
                    else
                        continue;
                }
                else
                {
                    if (startIndex == 1)
                        throw new FormatException($"传入参数格式有误,key:{key1}");
                    index = key1.Substring(startIndex, length - startIndex);
                    str = key1.Substring(length + 1);
                }
                Data[index] = str;
            }
        }
    }
}
