using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using iBlogs.Site.Core.Install.DTO;

namespace iBlogs.Site.Core.Common
{
    public static class ConfigDataHelper
    {
        private const string InstallFile = "Install.json";

        public static void UpdateDbInstallStatus(bool status)
        {
            var jObject = JsonConvert.DeserializeObject<JObject>(File.ReadAllText("appsettings.json"));
            jObject["DbInstalled"] = status;
            File.WriteAllText("appsettings.json", JsonConvert.SerializeObject(jObject, Formatting.Indented));
        }

        public static void UpdateAppSettings(string key, string value)
        {
            var jObject = JsonConvert.DeserializeObject<JObject>(File.ReadAllText("appsettings.json"));
            jObject[key] = value;
            File.WriteAllText("appsettings.json", JsonConvert.SerializeObject(jObject, Formatting.Indented));
        }

        public static void UpdateBuildNumber(string buildNumber)
        {
            UpdateAppSettings("BuildNumber", buildNumber);
        }
    }
}