using System.IO;
using iBlogs.Site.Core.Common;
using iBlogs.Site.Core.Option.Service;
using iBlogs.Site.Core.SqLite;

namespace iBlogs.Site.Core.Install
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
