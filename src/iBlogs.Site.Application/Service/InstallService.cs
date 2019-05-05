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

        public void InitializeDbAsync(string seedFileName=null)
        {
            if (seedFileName == null)
                seedFileName = "seed.sql";
            var sqlScript= File.ReadAllText(seedFileName);
            var initResult = false;
            using (var con=_baseRepository.DbConnection())
            {
               initResult=con.ExecuteAsync(sqlScript).Result>0;
            }
        }

        private void UpdateConfig()
        {
          
        }
    }
}
