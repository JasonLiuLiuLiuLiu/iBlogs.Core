using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace iBlogs.Site.Core.Common
{
    public static class ConfigDataHelper
    {
        public static void UpdateDbInstallStatus(bool status)
        {
            var jObject = JsonConvert.DeserializeObject<JObject>(File.ReadAllText("appsettings.json"));
            jObject[ConfigKey.DbInstalled] = status;
            File.WriteAllText("appsettings.json", JsonConvert.SerializeObject(jObject, Formatting.Indented));
        }
    }
}
