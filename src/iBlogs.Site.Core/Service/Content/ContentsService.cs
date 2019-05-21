using System;
using System.Data.Common;
using System.Linq;
using System.Text;
using Dapper;
using iBlogs.Site.Core.Entity;
using iBlogs.Site.Core.Extensions;
using iBlogs.Site.Core.Params;
using iBlogs.Site.Core.Response;
using iBlogs.Site.Core.Service.Common;
using iBlogs.Site.Core.SqLite;
using iBlogs.Site.Core.Utils;
using iBlogs.Site.Core.Utils.Extensions;
using Markdig.Extensions.Emoji;

namespace iBlogs.Site.Core.Service.Content
{
    public class ContentsService : IContentsService
    {
        private readonly DbConnection _sqlLite;
        private readonly IViewService _viewService;
        private readonly IMetasService _metasService;

        public ContentsService(IDbBaseRepository dbBaseRepository, IViewService viewService, IMetasService metasService)
        {
            _sqlLite = dbBaseRepository.DbConnection();
            _viewService = viewService;
            _metasService = metasService;
        }

        /**
         * 根据id或slug获取文章
         *
         * @param id 唯一标识
         */
        public Contents getContents(string id)
        {
            var contents = _sqlLite .QueryFirstOrDefault<Contents>($"select * from t_contents where slug='{id}'") ??
                           _sqlLite.QueryFirstOrDefault<Contents>($"select * from t_contents where cid={id}");
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

            int time = DateTime.Now.ToUnixTimestamp();
            contents.Created = time;
            contents.Modified = time;
            contents.Hits = 0;
            if (contents.FmtType.IsNullOrWhiteSpace())
            {
                contents.FmtType = "markdown";
            }
            var tags = contents.Tags;
            var categories = contents.Categories;

            _sqlLite.Execute(
                "INSERT INTO t_contents ( title, slug, created, modified, content, author_id, type, status, tags, categories, hits, comments_num, allow_comment, allow_ping, allow_feed,fmt_Type,thumb_img) VALUES" +
                " ( @Title, @Slug, @Created, @Modified, @Content, @AuthorId, @Type, @Status, @Tags, @Categories, @Hits, @CommentsNum, @AllowComment, @AllowPing, @AllowFeed,@FmtType,@ThumbImg)",
                contents);

            int cid = _sqlLite.QueryFirstOrDefault<int>("SELECT cid FROM t_contents WHERE title=@Title and created=@Created AND author_id=@AuthorId", contents);

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
            contents.Modified=DateTime.Now.ToUnixTimestamp();
            contents.Tags=contents.Tags ?? "";
            contents.Categories=contents.Categories ?? "";

            var cid = contents.Cid.ValueOrDefault();

            _sqlLite.Execute(
                "UPDATE t_contents SET title=@Title,slug=@Slug,modified= @Modified,content=@Content,status=@Status,tags=@Tags,categories=@Categories,allow_comment=@AllowComment,allow_ping=@AllowPing,allow_feed=@AllowFeed,fmt_Type=@FmtType,thumb_img=@ThumbImg where cid=@Cid",
                contents);

            var tags = contents.Tags;
            var categories = contents.Categories;

            if (null != contents.Type && !contents.Type.Equals(Types.PAGE))
            {
                _sqlLite.Execute("delete from t_relationships where cid=@cid",new{cid});
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
            var sqlBuilder = new StringBuilder();
            sqlBuilder.Append("select * from t_contents where 1=1");



            if (articleParam.Categories != null)
                sqlBuilder.Append(" and categories like '%@categories%' ");

            if (articleParam.Status != null)
                sqlBuilder.Append(" and status like '%@status%'");

            if (articleParam.Title != null)
                sqlBuilder.Append(" and title like '%@Title%'");

            if (articleParam.Type != null)
                sqlBuilder.Append(" and type=@Type");

            var count = _sqlLite.QueryCount(sqlBuilder.ToString(), articleParam);

            sqlBuilder.Append($" and {articleParam.OrderBy} NOT IN ( SELECT {articleParam.OrderBy} FROM t_contents ORDER BY {articleParam.OrderBy} {articleParam.OrderType} LIMIT {(articleParam.Page) * articleParam.Limit})");

            sqlBuilder.Append($" order by {articleParam.OrderBy} {articleParam.OrderType}");
            sqlBuilder.Append(" LIMIT @Limit ");

            var contents = _sqlLite.Query<Contents>(sqlBuilder.ToString(), articleParam).ToList();

            return new Page<Contents>(count, articleParam.Page + 1, articleParam.Limit, contents);
        }
    }
}
