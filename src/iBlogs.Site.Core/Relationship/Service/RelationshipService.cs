using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iBlogs.Site.Core.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace iBlogs.Site.Core.Relationship.Service
{
    public class RelationshipService : IRelationshipService
    {
        private readonly IRepository<Relationships> _repository;

        public RelationshipService(IRepository<Relationships> repository)
        {
            _repository = repository;
        }

        public void SaveOrUpdate(int cid,int mid)
        {
            _repository.InsertOrUpdate(new Relationships {Cid = cid, Mid = mid});
        }

        public void DeleteByContentId(int cid)
        {
            _repository.GetAll().Where(r => r.Cid == cid).ToList().ForEach(r=>_repository.Delete(r));
        }
    }
}
