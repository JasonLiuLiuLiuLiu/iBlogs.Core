using iBlogs.Site.Core.Common.Request;

namespace iBlogs.Site.Core.Blog.Comment.DTO
{
    public class CommentPageParam : PageParam
    {
        public int? Cid { get; set; }
        public CommentStatus Status { get; set; }
    }
}
