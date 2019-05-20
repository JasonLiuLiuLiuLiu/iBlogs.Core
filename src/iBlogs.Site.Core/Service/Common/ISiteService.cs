using System;
using System.Collections.Generic;
using iBlogs.Site.Core.Entity;
using iBlogs.Site.Core.Response;

namespace iBlogs.Site.Core.Service.Common
{
    public interface ISiteService
    {
        List<Comments> recentComments(int limit);
        List<Entity.Contents> getContens(string type, int limit);
        Statistics getStatistics();
        List<Archive> getArchives();
        Comments getComment(int coid);
        List<Metas> getMetas(string searchType, string type, int limit);
        Entity.Contents getNhContent(string type, long created);
        Page<Comments> getComments(int cid, int page, int limit);
        long getCommentCount(int cid);
        void cleanCache(string key);
    }
}