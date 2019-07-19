using System;
using System.Collections.Generic;

namespace iBlogs.Site.Core.Common.Extensions
{
    public static class ArgumentsExtension
    {
        private static readonly string DbService = "DbServer";
        private static readonly string DbName = "DbName";
        private static readonly string DbUID = "DbUID";
        private static readonly string DbPWD = "DbPWD";
        private static readonly string BuildNumber = "BuildNumber";
        private static readonly string RedisConStr = "RedisConStr";
        private static readonly string RabbitMqHost = "RabbitMqHost";
        private static readonly string RabbitMqPWD = "RabbitMqPWD";
        private static readonly string RabbitMqUID = "RabbitMqUID";
        private static readonly Dictionary<string, string> Data = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        public static string[] SetConfigInfo(this string[] args)
        {
            if (args == null || args.Length == 0)
                return args;

            Load(args);

            if (Data.ContainsKey(RabbitMqHost))
                ConfigDataHelper.UpdateAppsettings(RabbitMqHost, Data[RabbitMqHost]);

            if (Data.ContainsKey(RabbitMqPWD))
                ConfigDataHelper.UpdateAppsettings(RabbitMqPWD, Data[RabbitMqPWD]);

            if (Data.ContainsKey(RabbitMqUID))
                ConfigDataHelper.UpdateAppsettings(RabbitMqUID, Data[RabbitMqUID]);

            if (Data.ContainsKey(RedisConStr))
                ConfigDataHelper.UpdateRedisConStr(Data[RedisConStr]);

            if (!Data.ContainsKey(DbService) || !Data.ContainsKey(DbName) || !Data.ContainsKey(DbUID) || !Data.ContainsKey(DbPWD))
                return args;

            var connectString = $"Server={Data[DbService]};Database={Data[DbName]};uid={Data[DbUID]};pwd={Data[DbPWD]}";
            ConfigDataHelper.UpdateConnectionString("iBlogs", connectString);
            ConfigDataHelper.UpdateDbInstallStatus(true);
            Console.WriteLine("Set connection string from command line.");

            if (Data.ContainsKey(BuildNumber))
                ConfigDataHelper.UpdateBuildNumber(Data[BuildNumber]);

            return args;
        }

        private static void Load(IEnumerable<string> args)
        {
            using (IEnumerator<string> enumerator = args.GetEnumerator())
            {
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
}
