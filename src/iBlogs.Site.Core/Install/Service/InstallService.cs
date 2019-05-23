using System;
using System.IO;
using iBlogs.Site.Core.Common;
using iBlogs.Site.Core.Option.Service;

namespace iBlogs.Site.Core.Install.Service
{
    public class InstallService : IInstallService
    {
       
        private readonly IOptionService _optionService;

        public InstallService(IOptionService optionService)
        {
            _optionService = optionService;
        }

        public bool InitializeDb(string seedFileName = null)
        {
           throw new NotImplementedException();
        }
       
    }
}
