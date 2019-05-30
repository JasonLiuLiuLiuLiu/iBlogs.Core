using System.Collections.Generic;

namespace iBlogs.Site.Core.Comment.DTO
{
    public class CommentDto : Comments
    {
        public int levels;
        public List<Comments> children;
    }
}