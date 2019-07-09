using System;

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

        public static string[] SetConfigInfo(this string[] args)
        {
            if (args == null || args.Length == 0)
                return args;

            if(args.Arg(RedisConStr)!=null)
                ConfigDataHelper.UpdateRedisConStr(args.Arg(RedisConStr));

            if (args.Arg(DbService) == null || args.Arg(DbName) == null || args.Arg(DbUID) == null || args.Arg(DbPWD) == null)
                return args;

            var connectString = $"Server={args.Arg(DbService)};Database={args.Arg(DbName)};uid={args.Arg(DbUID)};pwd={args.Arg(DbPWD)}";
            ConfigDataHelper.UpdateConnectionString("iBlogs", connectString);
            ConfigDataHelper.UpdateDbInstallStatus(true);
            Console.WriteLine("Set connection string from command line.");

            if (args.Arg(BuildNumber) != null)
                ConfigDataHelper.UpdateBuildNumber(args.Arg(BuildNumber));

            return args;
        }

        private static string Arg(this string[] args, string name)
        {
            if (args == null || args.Length == 0)
                return null;

            foreach (var arg in args)
            {
                var argSplit = arg.Split('=');
                if (argSplit.Length != 2)
                    continue;
                if (argSplit[0].Equals(name, StringComparison.CurrentCultureIgnoreCase))
                    return argSplit[1];
            }

            return null;
        }
    }
}
