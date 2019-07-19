using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using iBlogs.Site.Core.Blog.Content;
using iBlogs.Site.Core.EntityFrameworkCore;

namespace iBlogs.Site.Core.Blog.Comment
{
    public class Comments : IEntityBase
    {
        [Key]
        public int Id { get; set; }

        public bool IsAuthor { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime Created { get; set; }

        public bool Deleted { get; set; }

        public int Cid { get; set; }

        [ForeignKey("Cid")]
        public Contents Article { get; set; }

        public string Author { get; set; }
        public int OwnerId { get; set; }
        public string Mail { get; set; }
        public string Url { get; set; }
        public string Ip { get; set; }
        public string Agent { get; set; }
        public string Content { get; set; }
        public string Type { get; set; }
        public CommentStatus Status { get; set; }
        public int Parent { get; set; }
    }
}