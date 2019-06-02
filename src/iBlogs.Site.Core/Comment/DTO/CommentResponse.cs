using System.Collections.Generic;

namespace iBlogs.Site.Core.Comment.DTO
{
    public class CommentResponse : Comments
    {
        public int Levels { get; set; }
        public List<CommentResponse> Children { get; set; }

        public string CommentAt(Comments com)
        {
            return "<a href=\"#comment-" + com.Id + "\">@" + com.Author + "</a>";
        }
    }
}