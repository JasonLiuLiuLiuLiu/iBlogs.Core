using iBlogs.Site.Core.Common.Request;

namespace iBlogs.Site.Core.Blog.Comment.DTO
{
    public class CommentParam : PageParam
    {
        public int Coid { get; set; }
        public CommentStatus Status { get; set; }
        public int ExcludeUid { get; set; }
    }
}