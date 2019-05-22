using System.Collections.Generic;
using iBlogs.Site.Core.Content;

namespace iBlogs.Site.Core.Comment.DTO
{
    public class CommentDto : Comments
    {
        public int levels;
        public List<Comments> children;
    }
}
