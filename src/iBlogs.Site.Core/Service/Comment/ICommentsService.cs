using iBlogs.Site.Core.Entity;
using iBlogs.Site.Core.Params;
using iBlogs.Site.Core.Response;

namespace iBlogs.Site.Core.Service.Comment
{
    public interface ICommentsService
    {
        void saveComment(Comments comments);
        void delete(int coid, int cid);
        Page<Comments> getComments(int cid, int page, int limit);
        long getCommentCount(int cid);
        Page<Comments> findComments(CommentParam commentParam);
    }
}