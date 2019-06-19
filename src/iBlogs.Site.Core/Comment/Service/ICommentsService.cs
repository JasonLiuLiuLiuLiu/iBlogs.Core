using iBlogs.Site.Core.Comment.DTO;
using iBlogs.Site.Core.Common.Response;

namespace iBlogs.Site.Core.Comment.Service
{
    public interface ICommentsService
    {
        int GetTotalCount();
        void SaveComment(Comments comments);
        void Delete(int? id);
        void UpdateComment(CommentParam param);
        Page<CommentResponse> GetComments(CommentPageParam param);
    }
}