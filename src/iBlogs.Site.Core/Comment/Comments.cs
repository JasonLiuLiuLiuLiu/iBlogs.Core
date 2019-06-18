using iBlogs.Site.Core.Content;
using iBlogs.Site.Core.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iBlogs.Site.Core.Comment
{
    public class Comments : IEntityBase
    {
        [Key]
        public int Id { get; set; }

        public bool IsAuthor { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime Created { get; set; }

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