using System;
using System.IO;
using iBlogs.Site.Core.Common.Extensions;
using Microsoft.Extensions.Configuration;

namespace iBlogs.Site.Core.Common
{
    public class IBlogsBoot
    {
        public static void Startup(IConfiguration configuration)
        {
            CheckDbInstallStatus(configuration);
        }
        private static void CheckDbInstallStatus(IConfiguration configuration)
        {
            if (File.Exists(Environment.CurrentDirectory + "\\" + configuration[ConfigKey.SqLiteDbFileName].IfNullReturnDefaultValue("iBlogs.db")))
                ConfigDataHelper.UpdateDbInstallStatus(true);
            else
                ConfigDataHelper.UpdateDbInstallStatus(false);
        }
    }
}
