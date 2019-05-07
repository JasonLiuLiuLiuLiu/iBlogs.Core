using System.IO;
using Dapper;
using iBlogs.Site.Application.SqLite;
using iBlogs.Site.Application.Utils;
using Microsoft.Extensions.Configuration;

namespace iBlogs.Site.Application.Service.Install
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
                ConfigDataHelper.UpdateDbInstallStatus(true);
            return initResult;
        }
       
    }
}
