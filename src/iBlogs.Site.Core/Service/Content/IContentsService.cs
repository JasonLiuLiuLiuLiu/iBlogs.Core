using System;
using iBlogs.Site.Core.Entity;
using iBlogs.Site.Core.Params;
using iBlogs.Site.Core.Response;

namespace iBlogs.Site.Core.Service.Content
{
    public interface IContentsService
    {
        Contents getContents(string id);
        int publish(Entity.Contents contents);
        void updateArticle(Entity.Contents contents);
        void delete(int cid);
        Page<Entity.Contents> getArticles(int mid, int page, int limit);
        Page<Entity.Contents> findArticles(ArticleParam articleParam);
    }
}