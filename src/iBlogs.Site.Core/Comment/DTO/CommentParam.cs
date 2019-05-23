using iBlogs.Site.Core.Common.DTO;
using iBlogs.Site.Core.Common.Request;

namespace iBlogs.Site.Core.Comment.DTO
{
    public class CommentParam:PageParam
    {
        public int ExcludeUid { get; set; }
    }
}
