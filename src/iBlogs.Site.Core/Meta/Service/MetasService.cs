using iBlogs.Site.Core.Common;
using iBlogs.Site.Core.Common.Extensions;
using iBlogs.Site.Core.Content;
using iBlogs.Site.Core.EntityFrameworkCore;
using iBlogs.Site.Core.Relationship.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using iBlogs.Site.Core.Meta.DTO;
using iBlogs.Site.Core.Relationship;

namespace iBlogs.Site.Core.Meta.Service
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

        public MetaDataViewModel LoadMetaDataViewModel(MetaType type,int topCount=0)
        {
            var query=_relRepository.GetAll()
                .Where(r=>r.Meta.Type==type)
                .Join(_repository.GetAll(), r => r.Mid, c => c.Id, (r, c) => new {r.Cid, c.Name})
                .GroupBy(g => g.Name)
                .Select(g => new KeyValuePair<string, int>(g.Key, g.Count()))
                .OrderByDescending(g=>g.Value);

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
            if (limit < 1 || limit > iBlogsConfig.MAX_POSTS)
            {
                limit = 10;
            }
            return _repository.GetAll().Where(m => m.Type == type).OrderByDescending(m => m.Id).Take(limit).ToList();
        }

        /**
         * 查询项目映射
         *
         * @param type 类型，tag or category
         */

        public Dictionary<string, List<Contents>> GetMetaMapping(string type)
        {
            return null;
        }

        private List<Contents> GetMetaContents(Metas m)
        {
            return null;
        }

        /**
         * 根据类型和名字查询项
         *
         * @param type 类型，tag or category
         * @param name 类型名
         */

        public Metas GetMeta(string type, string name)
        {
            return null;
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
                    saveOrUpdate(cid.ValueOrDefault(), name, type);
                }
            }
        }

        private void saveOrUpdate(int cid, string name, MetaType type)
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

        private void exec(string type, string name, Contents contents)
        {
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
            _repository.SaveChanges();
        }

        public List<Metas> getMetas(string searchType, string type, int limit)
        {
            if (stringKit.isBlank(searchType) || stringKit.isBlank(type))
            {
                return new List<Metas>();
            }

            if (limit < 1 || limit > iBlogsConfig.MAX_POSTS)
            {
                limit = 10;
            }

            //// 获取最新的项目
            //if (Types.RECENT_META.Equals(searchType))
            //{
            //    var sql =
            //        "select a.*, count(b.cid) as count from t_metas a left join `t_relationships` b on a.mid = b.mid "
            //        +
            //        "where a.type = @type group by a.mid order by count desc, a.mid desc limit @limit";

            //    return _.Query<Metas>(sql, new { type = type, limit = limit }).ToList();
            //}

            //// 随机获取项目
            //if (Types.RANDOM_META.Equals(searchType))
            //{
            //    List<int> mids = _sqLite.Query<int>(
            //        "select mid from t_metas where type = @type order by random() * mid limit @limit",
            //        new { type = type, limit = limit }).ToList();
            //    if (mids != null)
            //    {
            //        string sql =
            //            "select a.*, count(b.cid) as count from t_metas a left join `t_relationships` b on a.mid = b.mid "
            //            +
            //            "where a.mid in @mids group by a.mid order by count desc, a.mid desc";

            //        return _sqLite.Query<Metas>(sql, mids).ToList();
            //    }
            //}
            return new List<Metas>();
        }

        private string reMeta(string name, string metas)
        {
            return null;
        }
    }
}