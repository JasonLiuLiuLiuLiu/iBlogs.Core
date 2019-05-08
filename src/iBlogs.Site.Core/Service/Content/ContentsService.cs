using System;
using System.Linq;
using System.Text;
using Dapper;
using iBlogs.Site.Core.Entity;
using iBlogs.Site.Core.Params;
using iBlogs.Site.Core.Response;
using iBlogs.Site.Core.SqLite;
using iBlogs.Site.Core.Utils.Extensions;

namespace iBlogs.Site.Core.Service.Content
{
    public class ContentsService : IContentsService
    {
        private readonly ISqLiteBaseRepository _sqLiteBaseRepository;

        public ContentsService(ISqLiteBaseRepository sqLiteBaseRepository)
        {
            _sqLiteBaseRepository = sqLiteBaseRepository;
        }

        /**
         * 根据id或slug获取文章
         *
         * @param id 唯一标识
         */
        public Contents getContents(String id)
        {
            return null;
        }

        /**
         * 发布文章
         *
         * @param contents 文章对象
         */
        public int publish(Entity.Contents contents)
        {
            return 0;
        }

        /**
         * 编辑文章
         *
         * @param contents 文章对象
         */
        public void updateArticle(Entity.Contents contents)
        {

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
        public Page<Entity.Contents> getArticles(int mid, int page, int limit)
        {
            return null;
        }

        public Page<Entity.Contents> findArticles(ArticleParam articleParam)
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

            var count = _sqLiteBaseRepository.DbConnection().QueryCount(sqlBuilder.ToString(), articleParam);

            sqlBuilder.Append($" and {articleParam.OrderBy} NOT IN ( SELECT {articleParam.OrderBy} FROM t_contents ORDER BY title ASC LIMIT {(articleParam.Page) * articleParam.Limit})");

            sqlBuilder.Append(" order by ");
            sqlBuilder.Append(articleParam.OrderBy);
            sqlBuilder.Append(" LIMIT @Limit ");

            var contents = _sqLiteBaseRepository.DbConnection().Query<Contents>(sqlBuilder.ToString(), articleParam).ToList();

           return new Page<Contents>(count,articleParam.Page+1,articleParam.Limit,contents);
        }
    }
}
