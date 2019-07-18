using System.Collections.Generic;
using System.Threading.Tasks;
using iBlogs.Site.Core.Blog.Content.DTO;
using iBlogs.Site.Core.Blog.Meta;
using iBlogs.Site.Core.Common.Request;
using iBlogs.Site.Core.Common.Response;

namespace iBlogs.Site.Core.Blog.Content.Service
{
    public interface IContentsService
    {
        ContentResponse GetContents(string id);

        int Publish(ContentInput contents);

        int UpdateArticle(ContentInput contents);

        void Delete(int cid);

        Page<ContentResponse> GetArticles(int mid, int page, int limit);

        Page<ContentResponse> FindArticles(ArticleParam articleParam);

        ContentResponse GetPre(int id);

        ContentResponse GetNext(int id);

        Page<ContentResponse> FindContentByMeta(MetaType type, string value, ArticleParam articleParam);

        Page<Archive> GetArchive(PageParam pageParam);

        void UpdateCommentCount(int cid,int updateCount);
        Task<List<ContentResponse>> GetContent(int limit);
    }
}