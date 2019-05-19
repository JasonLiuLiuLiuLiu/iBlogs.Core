using System.IO;
using Dapper;
using iBlogs.Site.Core.Service.Options;
using iBlogs.Site.Core.SqLite;
using iBlogs.Site.Core.Utils;
using Microsoft.Extensions.Configuration;

namespace iBlogs.Site.Core.Service.Install
{
    public class InstallService : IInstallService
    {
        private readonly IDbBaseRepository _baseRepository;
        private readonly IOptionService _optionService;

        public InstallService(IDbBaseRepository baseRepository, IOptionService optionService)
        {
            _baseRepository = baseRepository;
            _optionService = optionService;
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

            if (initResult)
            {
                ConfigDataHelper.UpdateDbInstallStatus(true);
                _optionService.ReLoad();
            }
            return initResult;
        }
       
    }
}
