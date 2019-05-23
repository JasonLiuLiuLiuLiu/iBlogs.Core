using System;
using System.Data.Common;
using System.Linq;
using System.Text;
using iBlogs.Site.Core.Comment;
using iBlogs.Site.Core.Common;
using iBlogs.Site.Core.Common.DTO;
using iBlogs.Site.Core.Common.Extensions;
using iBlogs.Site.Core.Common.Response;
using iBlogs.Site.Core.Common.Service;
using iBlogs.Site.Core.Content.DTO;
using iBlogs.Site.Core.EntityFrameworkCore;
using iBlogs.Site.Core.Meta.Service;
using iBlogs.Site.Core.Relationship.Service;

namespace iBlogs.Site.Core.Content.Service
{
    public class ContentsService : IContentsService
    {
        private readonly IViewService _viewService;
        private readonly IMetasService _metasService;
        private readonly IRepository<Contents> _repository;
        private readonly IRelationshipService _relationshipService;

        public ContentsService( IViewService viewService, IMetasService metasService, IRepository<Contents> repository, IRelationshipService relationshipService)
        {
            _viewService = viewService;
            _metasService = metasService;
            _repository = repository;
            _relationshipService = relationshipService;
        }

        /**
         * 根据id或slug获取文章
         *
         * @param id 唯一标识
         */
        public Contents getContents(string id)
        {
            var contents = _repository.GetAll().FirstOrDefault(c => c.Slug == id) ?? _repository.FirstOrDefault(int.Parse(id));
            _viewService.Set_current_article(contents);
            if (contents.FmtType.IsNullOrWhiteSpace())
            {
                contents.FmtType = "markdown";
            }
            return contents;
        }

        /**
         * 发布文章
         *
         * @param contents 文章对象
         */
        public int publish(Contents contents)
        {
            if (null == contents.AuthorId)
            {
                throw new Exception("请登录后发布文章");
            }
            contents.Created = DateTime.Now;
            contents.Modified = DateTime.Now;
            contents.Hits = 0;
            if (contents.FmtType.IsNullOrWhiteSpace())
            {
                contents.FmtType = "markdown";
            }
            var tags = contents.Tags;
            var categories = contents.Categories;

            var cid = _repository.InsertOrUpdateAndGetId(contents);

            _metasService.saveMetas(cid, tags, Types.TAG);
            _metasService.saveMetas(cid, categories, Types.CATEGORY);

            return cid;
        }

        /**
         * 编辑文章
         *
         * @param contents 文章对象
         */
        public void updateArticle(Contents contents)
        {
            contents.Modified=DateTime.Now;
            contents.Tags=contents.Tags ?? "";
            contents.Categories=contents.Categories ?? "";

            var cid = _repository.InsertOrUpdateAndGetId(contents);

            var tags = contents.Tags;
            var categories = contents.Categories;

            if (null != contents.Type && !contents.Type.Equals(Types.PAGE))
            {
                _relationshipService.DeleteByContentId(cid);
            }

            _metasService.saveMetas(cid, tags, Types.TAG);
            _metasService.saveMetas(cid, categories, Types.CATEGORY);
        }

        /**
         * 根据文章id删除
         *
         * @param cid 文章id
         */
        public void delete(int cid)
        {

        }

        /**
         * 查询分类/标签下的文章归档
         *
         * @param mid   分类、标签id
         * @param page  页码
         * @param limit 每页条数
         * @return
         */
        public Page<Contents> getArticles(int mid, int page, int limit)
        {
            return null;
        }

        public Page<Contents> findArticles(ArticleParam articleParam)
        {
            //var sqlBuilder = new StringBuilder();
            //sqlBuilder.Append("select * from t_contents where 1=1");



            //if (articleParam.Categories != null)
            //    sqlBuilder.Append(" and categories like '%@categories%' ");

            //if (articleParam.Status != null)
            //    sqlBuilder.Append(" and status like '%@status%'");

            //if (articleParam.Title != null)
            //    sqlBuilder.Append(" and title like '%@Title%'");

            //if (articleParam.Type != null)
            //    sqlBuilder.Append(" and type=@Type");

            //var count = _sqlLite.QueryCount(sqlBuilder.ToString(), articleParam);

            //sqlBuilder.Append($" and {articleParam.OrderBy} NOT IN ( SELECT {articleParam.OrderBy} FROM t_contents ORDER BY {articleParam.OrderBy} {articleParam.OrderType} LIMIT {(articleParam.Page) * articleParam.Limit})");

            //sqlBuilder.Append($" order by {articleParam.OrderBy} {articleParam.OrderType}");
            //sqlBuilder.Append(" LIMIT @Limit ");

            //var contents = _sqlLite.Query<Contents>(sqlBuilder.ToString(), articleParam).ToList();

            //return new Page<Contents>(count, articleParam.Page + 1, articleParam.Limit, contents);
            throw new NotImplementedException();
        }
    }
}
