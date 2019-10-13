using System.Linq;
using iBlogs.Site.Core.Storage;
using Microsoft.EntityFrameworkCore;

namespace iBlogs.Site.Core.Blog.Relationship.Service
{
    public class RelationshipService : IRelationshipService
    {
        private readonly IRepository<Relationships> _repository;

        public RelationshipService(IRepository<Relationships> repository)
        {
            _repository = repository;
        }

        public int GetContentCountByMetaDataId(int mid)
        {
            return _repository.GetAll().Count(u => u.Mid == mid);
        }

        public void SaveOrUpdate(int cid, int mid)
        {
            _repository.InsertOrUpdate(new Relationships { Cid = cid, Mid = mid });
            _repository.SaveChanges();
        }

        public void DeleteByContentId(int cid)
        {
            _repository.GetAll().Where(r => r.Cid == cid).ForEachAsync(r => _repository.Delete(r)).Wait();
            _repository.SaveChanges();
        }

        public void DeleteByMetaId(int id)
        {
            _repository.GetAll().Where(r => r.Mid == id).ForEachAsync(r => _repository.Delete(r)).Wait();
            _repository.SaveChanges();
        }
    }
}