using System;
using iBlogs.Site.Application.Entity;
using iBlogs.Site.Application.Params;
using iBlogs.Site.Application.Response;

namespace iBlogs.Site.Application.Service.Content
{
    public interface IContentsService
    {
        Contents getContents(String id);
        int publish(Entity.Contents contents);
        void updateArticle(Entity.Contents contents);
        void delete(int cid);
        Page<Entity.Contents> getArticles(int mid, int page, int limit);
        Page<Entity.Contents> findArticles(ArticleParam articleParam);
    }
}