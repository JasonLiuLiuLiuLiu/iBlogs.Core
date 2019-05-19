using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Text;
using Dapper;
using iBlogs.Site.Core.Entity;
using iBlogs.Site.Core.Extensions;
using iBlogs.Site.Core.Response;
using iBlogs.Site.Core.SqLite;
using iBlogs.Site.Core.Utils;
using Microsoft.Data.Sqlite;

namespace iBlogs.Site.Core.Service.Common
{
    public class SiteService : ISiteService
    {
        private readonly DbConnection _sqLite;

        public SiteService(IDbBaseRepository db)
        {
            _sqLite = db.DbConnection();
        }

        /**
         * 最新收到的评论
         *
         * @param limit 评论数
         */
        public List<Comments> recentComments(int limit)
        {
            return null;
        }

        /**
         * 根据类型获取文章列表
         *
         * @param type 最新,随机
         * @param limit 获取条数
         */
        public List<Entity.Contents> getContens(string type, int limit)
        {

            return null;
        }

        /**
         * 获取后台统计数据
         */
        public Statistics getStatistics()
        {

            return null;
        }

        /**
         * 查询文章归档
         */
        public List<Archive> getArchives()
        {
            return null;
        }

        private Archive parseArchive(Archive archive)
        {
            return null;
        }

        /**
         * 查询一条评论
         *
         * @param coid 评论主键
         */
        public Comments getComment(int coid)
        {
            return null;
        }

        /**
         * 获取分类/标签列表
         */
        public List<Metas> getMetas(string searchType, string type, int limit)
        {
            if (stringKit.isBlank(searchType) || stringKit.isBlank(type))
            {
                return new List<Metas>();
            }

            if (limit < 1 || limit > iBlogsConst.MAX_POSTS)
            {
                limit = 10;
            }

            // 获取最新的项目
            if (Types.RECENT_META.Equals(searchType))
            {
                var sql =
                    "select a.*, count(b.cid) as count from t_metas a left join `t_relationships` b on a.mid = b.mid "
                    +
                    "where a.type = @type group by a.mid order by count desc, a.mid desc limit @limit";

                return _sqLite.Query<Metas>(sql, new { type = type, limit = limit }).ToList();
            }

            // 随机获取项目
            if (Types.RANDOM_META.Equals(searchType))
            {
                List<int> mids = _sqLite.Query<int>(
                "select mid from t_metas where type = @type order by random() * mid limit @limit",
                new { type = type, limit = limit }).ToList();
                if (mids != null)
                {
                    string sql =
                        "select a.*, count(b.cid) as count from t_metas a left join `t_relationships` b on a.mid = b.mid "
                        +
                        "where a.mid in @mids group by a.mid order by count desc, a.mid desc";

                    return _sqLite.Query<Metas>(sql, mids).ToList();
                }
            }
            return new List<Metas>();
        }

        /**
         * 获取相邻的文章
         *
         * @param type 上一篇:prev | 下一篇:next
         * @param created 当前文章创建时间
         */
        public Contents getNhContent(string type, long created)
        {
            return null;
        }

        /**
         * 获取文章的评论
         *
         * @param cid 文章id
         * @param page 页码
         * @param limit 每页条数
         */
        public Page<Comments> getComments(int cid, int page, int limit)
        {
            return null;
        }

        /**
         * 获取文章的评论总数
         *
         * @param cid 文章id
         */
        public long getCommentCount(int cid)
        {
            return 0;
        }

        /**
         * 清楚缓存
         *
         * @param key 缓存key
         */
        public void cleanCache(string key)
        {

        }

    }
}
