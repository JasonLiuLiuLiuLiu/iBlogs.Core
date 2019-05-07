using System;
using System.Collections.Generic;
using iBlogs.Site.Application.Entity;
using iBlogs.Site.Application.Response;

namespace iBlogs.Site.Application.Service.Common
{
    public interface ISiteService
    {
        List<Comments> recentComments(int limit);
        List<Entity.Contents> getContens(String type, int limit);
        Statistics getStatistics();
        List<Archive> getArchives();
        Comments getComment(int coid);
        List<Metas> getMetas(String searchType, String type, int limit);
        Entity.Contents getNhContent(String type, int created);
        Page<Comments> getComments(int cid, int page, int limit);
        long getCommentCount(int cid);
        void cleanCache(String key);
    }
}