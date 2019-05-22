using System;
using System.Collections.Generic;
using System.Data.Common;
using iBlogs.Site.Core.Comment;
using iBlogs.Site.Core.Common.Extensions;
using iBlogs.Site.Core.SqLite;

namespace iBlogs.Site.Core.Meta.Service
{
    public class MetasService : IMetasService
    {
        private readonly DbConnection _sqlite;

        public MetasService(IDbBaseRepository db)
        {
            _sqlite = db.DbConnection();
        }

        /**
        * 根据类型查询项目列表
        *
        * @param type 类型，tag or category
        */
        public List<Metas> getMetas(string type)
        {
            return null;
        }

        /**
         * 查询项目映射
         *
         * @param type 类型，tag or category
         */
        public Dictionary<string, List<Contents>> getMetaMapping(string type)
        {
            return null;
        }

        private List<Contents> getMetaContents(Metas m)
        {
            return null;
        }

        /**
         * 根据类型和名字查询项
         *
         * @param type 类型，tag or category
         * @param name 类型名
         */
        public Metas getMeta(string type, string name)
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
        public void saveMetas(int? cid, string names, string type)
        {
            if (null == cid)
            {
                throw new Exception("项目关联id不能为空");
            }
            if (!names.IsNullOrWhiteSpace() && !type.IsNullOrWhiteSpace())
            {
                var nameArr = names.Split(",");
                foreach (var name in nameArr)
                {
                    saveOrUpdate(cid.ValueOrDefault(), name, type);
                }
            }
        }

        private void saveOrUpdate(int cid, string name, string type)
        {
            var metas = _sqlite.QueryFirstOrDefault<Metas>(
                "select * from t_metas where name=@name and type=@type",new {cid,name,type});
            int mid;
            if (null != metas) {
                mid = metas.Mid;
            } else {
                metas = new Metas {Slug = name, Name = name, Type = type};
                _sqlite.Execute("insert into t_metas (name,type) values (@Name,@Type)", metas);
                mid = _sqlite.QueryFirstOrDefault<int>("select mid from t_metas where name=@name and type=@type", new { cid, name, type });
            }
            if (mid != 0)
            {
                var count = _sqlite.QueryFirstOrDefault<int>("select count(1) from t_relationships where cid=@cid and mid=@mid",
                    new {cid, mid});
                if (count == 0)
                {
                    _sqlite.Execute("insert into t_relationships (cid,mid) values (@cid,@mid)", new { cid, mid });
                }
            }
        }

        /**
         * 删除项目
         *
         * @param mid 项目id
         */
        public void delete(int mid)
        {

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
        public void saveMeta(string type, string name, int? mid)
        {
            if (type.IsNullOrWhiteSpace() || name.IsNullOrWhiteSpace())
            {
                return;
            }

            if (null != mid)
            {

                _sqlite.Execute("update t_metas set name=@name where mid=@mid", new { name, mid = mid.Value });
            }
            else
            {
                var metas = _sqlite.QueryFirstOrDefault<Metas>("select * from t_metas WHERE type=@type and name=@name", new { type, name });
                if (null != metas)
                {
                    throw new Exception("已经存在该项");
                }
                _sqlite.Execute("insert into t_metas (name,type) values (@name,@type)", new { name, type });
            }
        }

        private string reMeta(string name, string metas)
        {
            return null;
        }
    }
}
