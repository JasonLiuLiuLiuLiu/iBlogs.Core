using iBlogs.Site.Core.Comment.DTO;
using iBlogs.Site.Core.Common.DTO;
using iBlogs.Site.Core.Common.Response;
using iBlogs.Site.Core.Content;

namespace iBlogs.Site.Core.Comment.Service
{
    public interface ICommentsService
    {
        int GetTotalCount();
        void SaveComment(Comments comments);
        void delete(int coid, int cid);
        Page<CommentResponse> GetComments(CommentPageParam param);
        long getCommentCount(int cid);
        Page<Comments> findComments(CommentParam commentParam);
    }
}