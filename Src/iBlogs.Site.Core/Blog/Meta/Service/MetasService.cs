using System;
using System.Collections.Generic;
using System.Linq;
using iBlogs.Site.Core.Blog.Meta.DTO;
using iBlogs.Site.Core.Blog.Relationship;
using iBlogs.Site.Core.Blog.Relationship.Service;
using iBlogs.Site.Core.Common.Extensions;
using iBlogs.Site.Core.Option;
using iBlogs.Site.Core.Storage;

namespace iBlogs.Site.Core.Blog.Meta.Service
{
    public class MetasService : IMetasService
    {
        private readonly IRepository<Metas> _repository;
        private readonly IRelationshipService _relationshipService;
        private readonly IRepository<Relationships> _relRepository;

        public MetasService(IRepository<Metas> repository, IRelationshipService relationshipService, IRepository<Relationships> relRepository)
        {
            _repository = repository;
            _relationshipService = relationshipService;
            _relRepository = relRepository;
        }

        public MetaDataViewModel LoadMetaDataViewModel(MetaType type, int topCount = 0)
        {
            var query = _relRepository.GetAll()
                .Where(r => r.Meta.Type == type)
                .Join(_repository.GetAll(), r => r.Mid, c => c.Id, (r, c) => new { r.Cid, c.Name })
                .GroupBy(g => g.Name)
                .Select(g => new KeyValuePair<string, int>(g.Key, g.Count()))
                .OrderByDescending(g => g.Value);

            if (topCount > 0)
                query = query.Take(topCount).OrderByDescending(g => g.Value);

            var countPair = query.ToList();

            var count = _repository.GetAll().Count(t => t.Type == MetaType.Tag);
            return new MetaDataViewModel
            {
                Total = count,
                Data = countPair,
            };
        }

        /**
        * 根据类型查询项目列表
        *
        * @param type 类型，tag or category
        */

        public List<Metas> GetMetas(MetaType type, int limit = 0)
        {
            if (limit < 1 || limit > int.Parse(ConfigData.Get(ConfigKey.MaxPage, 999.ToString())))
            {
                limit = 10;
            }
            return _repository.GetAll().Where(m => m.Type == type).OrderByDescending(m => m.Id).Take(limit).ToList();
        }

        /**
         * 保存多个项目
         *
         * @param cid   文章id
         * @param names 类型名称列表
         * @param type  类型，tag or category
         */

        public void SaveMetas(int? cid, string names, MetaType type)
        {
            if (null == cid)
            {
                throw new Exception("项目关联id不能为空");
            }
            if (!names.IsNullOrWhiteSpace())
            {
                var nameArr = names.Split(",");
                foreach (var name in nameArr)
                {
                    SaveOrUpdate(cid.ValueOrDefault(), name, type);
                }
            }
        }

        /**
         * 删除项目
         *
         * @param mid 项目id
         */

        public void Delete(int id)
        {
            _repository.Delete(id);
            _relationshipService.DeleteByMetaId(id);
        }
        /**
         * 保存项目
         *
         * @param type
         * @param name
         * @param mid
         */

        public void SaveMeta(MetaType type, string name, int? mid)
        {
            if (name.IsNullOrWhiteSpace())
            {
                return;
            }
            _repository.InsertOrUpdate(new Metas() { Id = mid.ValueOrDefault(), Name = name, Type = type });
        }

        private void SaveOrUpdate(int cid, string name, MetaType type)
        {
            var metas = _repository.GetAll().Where(m => m.Name == name).FirstOrDefault(m => m.Type == type);
            int mid;
            if (null != metas)
            {
                mid = metas.Id;
            }
            else
            {
                metas = new Metas { Slug = name, Name = name, Type = type };
                mid = _repository.InsertAndGetId(metas);
            }
            if (mid != 0)
            {
                _relationshipService.SaveOrUpdate(cid, mid);
            }
            metas.Count = _relationshipService.GetContentCountByMetaDataId(mid);
            _repository.Update(metas);
        }
    }
}