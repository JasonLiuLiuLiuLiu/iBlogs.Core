using iBlogs.Site.Core.Common.Response;
using iBlogs.Site.Core.Content.DTO;

namespace iBlogs.Site.Core.Content.Service
{
    public interface IContentsService
    {
        Contents getContents(string id);

        int publish(ContentInput contents);

        void updateArticle(ContentInput contents);

        void delete(int cid);

        Page<Contents> getArticles(int mid, int page, int limit);

        Page<Contents> findArticles(ArticleParam articleParam);
    }
}