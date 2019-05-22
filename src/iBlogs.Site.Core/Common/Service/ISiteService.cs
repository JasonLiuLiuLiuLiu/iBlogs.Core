using System.Collections.Generic;
using iBlogs.Site.Core.Comment;
using iBlogs.Site.Core.Common.DTO;
using iBlogs.Site.Core.Content;
using iBlogs.Site.Core.Content.DTO;
using iBlogs.Site.Core.Meta;

namespace iBlogs.Site.Core.Common.Service
{
    public interface ISiteService
    {
        List<Comments> recentComments(int limit);
        List<Contents> getContens(string type, int limit);
        Statistics getStatistics();
        List<Archive> getArchives();
        Comments getComment(int coid);
        List<Metas> getMetas(string searchType, string type, int limit);
        Contents getNhContent(string type, long created);
        Page<Comments> getComments(int cid, int page, int limit);
        long getCommentCount(int cid);
        void cleanCache(string key);
    }
}