using System;
using System.Data;
using System.Threading.Tasks;
using iBlogs.Site.Core.Common.Request;
using iBlogs.Site.Core.Common.Response;
using iBlogs.Site.Core.EntityFrameworkCore;

namespace iBlogs.Site.Core.Attach.Service
{
    public class AttachService : IAttachService
    {
        private readonly IRepository<Attachment> _repository;

        public AttachService(IRepository<Attachment> repository)
        {
            _repository = repository;
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
    }
}
