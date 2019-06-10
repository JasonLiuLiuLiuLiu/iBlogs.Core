using System.Collections.Generic;
using iBlogs.Site.Core.Comment;
using iBlogs.Site.Core.Common.Extensions;
using iBlogs.Site.Core.Common.Response;
using iBlogs.Site.Core.Content;
using iBlogs.Site.Core.Content.DTO;
using iBlogs.Site.Core.Meta;
using iBlogs.Site.Core.Meta.Service;

namespace iBlogs.Site.Core.Common.Service
{
    public class SiteService : ISiteServiceRe
    {
        private readonly IMetasService _metasService;

        public SiteService(IMetasService metasService)
        {
            _metasService = metasService;
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
        public List<Contents> getContens(string type, int limit)
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
        public List<Metas> getMetas(string type, int limit)
        {
            return _metasService.GetMetas(type, limit);
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

    }
}
