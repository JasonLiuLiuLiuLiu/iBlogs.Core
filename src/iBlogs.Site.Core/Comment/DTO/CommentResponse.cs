using System;
using System.Collections.Generic;
using iBlogs.Site.Core.Content;

namespace iBlogs.Site.Core.Comment.DTO
{
    public class CommentResponse 
    {
      
        public int Id { get; set; }

        public bool IsAuthor { get; set; }
        public DateTime Created { get; set; }

        public int Cid { get; set; }
       
        public Contents Article { get; set; }

        public string Author { get; set; }
        public int OwnerId { get; set; }
        public string Mail { get; set; }
        public string Url { get; set; }
        public string Ip { get; set; }
        public string Agent { get; set; }
        public string Content { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public int Parent { get; set; }

        public int Levels { get; set; }

        public List<CommentResponse> Children { get; set; }

        public string CommentAt(CommentResponse com)
        {
            return "<a href=\"#comment-" + com.Id + "\">@" + com.Author + "</a>";
        }
    }
}