using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using iBlogs.Site.Application.SqLite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace iBlogs.Site.Application.Service
{
    public class InstallService : IInstallService
    {
        private ISqLiteBaseRepository _baseRepository;
        private IConfiguration _configuration;

        public InstallService(ISqLiteBaseRepository baseRepository, IConfiguration configuration)
        {
            _baseRepository = baseRepository;
            _configuration = configuration;
        }

        public bool InitializeDb(string seedFileName = null)
        {
            if (seedFileName == null)
                seedFileName = "seed.sql";
            var sqlScript = File.ReadAllText(seedFileName);
            var initResult = false;
            using (var con = _baseRepository.DbConnection())
            {
                initResult = con.ExecuteAsync(sqlScript).Result > 0;
            }
            if(initResult)
                Update();
            return initResult;
        }
        public void Update()
        {
            var jObject = JsonConvert.DeserializeObject<JObject>(File.ReadAllText("appsettings.json"));
            jObject[ConfigKey.DbInstalled] = true;
            File.WriteAllText("appsettings.json", JsonConvert.SerializeObject(jObject, Formatting.Indented));
        }
    }
}
