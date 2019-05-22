using iBlogs.Site.Core.Common.DTO;

namespace iBlogs.Site.Core.Comment.DTO
{
    public class CommentParam:PageParam
    {
        public int ExcludeUid { get; set; }
    }
}
