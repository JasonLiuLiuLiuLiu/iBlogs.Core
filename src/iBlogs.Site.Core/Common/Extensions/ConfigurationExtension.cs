using System;
using Microsoft.Extensions.Configuration;

namespace iBlogs.Site.Core.Common.Extensions
{
    public static class ConfigurationExtension
    {
        private static readonly string DbService = "DbServer";
        private static readonly string DbName = "DbName";
        private static readonly string DbUID = "DbUID";
        private static readonly string DbPWD = "DbPWD";
        public static IConfiguration ConfigAppSettingFromCmd(this IConfiguration configuration)
        {
            if (configuration[DbService] == null || configuration[DbName] == null || configuration[DbUID] == null || configuration[DbPWD] == null)
                return configuration;

            var connectString = $"Server={configuration[DbService]};Database={configuration[DbName]};uid={configuration[DbUID]};pwd={configuration[DbPWD]}";
            Console.WriteLine(connectString);
            ConfigDataHelper.UpdateConnectionString("iBlogs", connectString);
            ConfigDataHelper.UpdateDbInstallStatus(true);
            Console.WriteLine("Set connection string from command line.");

            return configuration;
        }
    }
}
