using iBlogs.Site.Core.Blog.Comment.DTO;
using iBlogs.Site.Core.Common.Response;

namespace iBlogs.Site.Core.Blog.Comment.Service
{
    public interface ICommentsService
    {
        int GetTotalCount();
        void SaveComment(Comments comments);
        void Reply(Comments comments);
        void Delete(int? id);
        void UpdateComment(CommentParam param);
        Page<CommentResponse> GetComments(CommentPageParam param);
    }
}