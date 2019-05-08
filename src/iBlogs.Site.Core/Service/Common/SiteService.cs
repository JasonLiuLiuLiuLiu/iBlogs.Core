using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using iBlogs.Site.Core.Entity;
using iBlogs.Site.Core.Extensions;
using iBlogs.Site.Core.Response;

namespace iBlogs.Site.Core.Service.Common
{
    public class SiteService : ISiteService
    {

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
        public List<Entity.Contents> getContens(String type, int limit)
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
        public List<Metas> getMetas(String searchType, String type, int limit)
        {

            return null;
        }

        /**
         * 获取相邻的文章
         *
         * @param type 上一篇:prev | 下一篇:next
         * @param created 当前文章创建时间
         */
        public Entity.Contents getNhContent(String type, int created)
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
        public void cleanCache(String key)
        {

        }

    }
}
