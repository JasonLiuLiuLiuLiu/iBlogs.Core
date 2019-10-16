using System.Linq;
using iBlogs.Site.Core.Storage;

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
        }

        public void DeleteByContentId(int cid)
        {
            foreach (var relationship in _repository.GetAll().Where(r => r.Cid == cid))
            {
                _repository.Delete(relationship);
            }
        }

        public void DeleteByMetaId(int id)
        {
            foreach (var relationship in _repository.GetAll().Where(r => r.Mid == id))
            {
                _repository.Delete(relationship);
            }
        }
    }
}