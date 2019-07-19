using System;
using System.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using iBlogs.Site.Core.Install.DTO;
using MySql.Data.MySqlClient;

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

        public static void UpdateAppsettings(string key, string value)
        {
            var jObject = JsonConvert.DeserializeObject<JObject>(File.ReadAllText("appsettings.json"));
            jObject[key] = value;
            File.WriteAllText("appsettings.json", JsonConvert.SerializeObject(jObject, Formatting.Indented));
        }

        public static void UpdateBuildNumber(string buildNumber)
        {
            var jObject = JsonConvert.DeserializeObject<JObject>(File.ReadAllText("appsettings.json"));
            jObject["BuildNumber"] = buildNumber;
            File.WriteAllText("appsettings.json", JsonConvert.SerializeObject(jObject, Formatting.Indented));
        }

        public static void UpdateRedisConStr(string redisConStr)
        {
            var jObject = JsonConvert.DeserializeObject<JObject>(File.ReadAllText("appsettings.json"));
            jObject["RedisConnectionString"] = redisConStr;
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
            if (param == null)
                return;
            File.WriteAllText(InstallFile, JsonConvert.SerializeObject(param));
        }

        public static InstallParam ReadInstallParam()
        {
            return JsonConvert.DeserializeObject<InstallParam>(File.ReadAllText(InstallFile));
        }

        public static void DeleteInstallParamFile()
        {
            if (File.Exists(InstallFile))
                File.Delete(InstallFile);
        }

        public static bool TryGetConnectionString(string connectionName, out string connectionString)
        {
            var jObject = JsonConvert.DeserializeObject<JObject>(File.ReadAllText("appsettings.json"));
            connectionString = jObject["ConnectionStrings"][connectionName].ToString();
            return CheckConnection(connectionString);
        }

        //https://stackoverflow.com/questions/17195200/check-mysql-db-connection
        private static bool CheckConnection(string connectionString)
        {
            var connInfo = connectionString;
            bool isConn = false;
            MySqlConnection conn = null;
            try
            {
                conn = new MySqlConnection(connInfo);
                conn.Open();
                isConn = true;
            }
            catch (ArgumentException aEx)
            {
                Console.WriteLine("Check the Connection String.");
                Console.WriteLine(aEx.Message);
                Console.WriteLine(aEx.ToString());
            }
            catch (MySqlException ex)
            {
                string sqlErrorMessage = "Message: " + ex.Message + "\n" +
                "Source: " + ex.Source + "\n" +
                "Number: " + ex.Number;
                Console.WriteLine(sqlErrorMessage);

                isConn = false;
                switch (ex.Number)
                {
                    //http://dev.mysql.com/doc/refman/5.0/en/error-messages-server.html
                    case 1042: // Unable to connect to any of the specified MySQL hosts (Check Server,Port)
                        break;
                    case 0: // Access denied (Check DB name,username,password)
                        break;
                }
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return isConn;
        }
    }
}