using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using iBlogs.Site.Core.Common;
using iBlogs.Site.Core.Common.Request;
using iBlogs.Site.Core.Common.Response;
using iBlogs.Site.Core.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;

namespace iBlogs.Site.Core.Blog.Attach.Service
{
    public class AttachService : IAttachService
    {
        private readonly IRepository<Attachment> _repository;
        private readonly IHostingEnvironment _env;

        public AttachService(IRepository<Attachment> repository, IHostingEnvironment env)
        {
            _repository = repository;
            _env = env;
        }

        public async Task<bool> Save(Attachment attachment)
        {
            if (attachment == null)
                throw new NullReferenceException();

            var result = await _repository.InsertOrUpdateAndGetIdAsync(attachment);
            return result != 0;
        }

        public Page<Attachment> GetPage(PageParam param)
        {
            return _repository.Page(_repository.GetAll(), param);
        }

        public void Delete(int id)
        {
            var attach = _repository.FirstOrDefault(id);
            if(attach==null)
                return;
            var filePath = _env.WebRootPath + attach.FKey;
            var newFileName = BlogsUtils.GetFileName(attach.FKey);
            var thumbnailFilePath = _env.WebRootPath + attach.FKey.Replace(newFileName, "thumbnail_" + newFileName);
            _repository.Delete(attach);
            _repository.SaveChanges();
            File.Delete(filePath);
            File.Delete(thumbnailFilePath);
        }

        public int GetTotalCount()
        {
            return _repository.GetAll().Count();
        }
    }
}
