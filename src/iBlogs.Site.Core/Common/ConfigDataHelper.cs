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

        public static void UpdateConnectionString(string connectionName, string value)
        {
            var jObject = JsonConvert.DeserializeObject<JObject>(File.ReadAllText("appsettings.json"));
            jObject["ConnectionStrings"][connectionName] = value;
            File.WriteAllText("appsettings.json", JsonConvert.SerializeObject(jObject, Formatting.Indented));
        }

        public static void SaveInstallParam(InstallParam param)
        {
            if(param==null)
                return;
            File.WriteAllText(InstallFile, JsonConvert.SerializeObject(param));
        }

        public static InstallParam ReadInstallParam()
        {
            return JsonConvert.DeserializeObject<InstallParam>(File.ReadAllText(InstallFile));
        }

        public static void DeleteInstallParamFile()
        {
            if(File.Exists(InstallFile))
                File.Delete(InstallFile);
        }
    }
}