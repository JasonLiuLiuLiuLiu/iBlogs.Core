using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using iBlogs.Site.Core.Security;
using iBlogs.Site.Core.Storage;

namespace iBlogs.Site.Core.Blog.Content
{
    [Serializable]
    public class Contents : IEntityBase
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }
        public string Slug { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime Modified { get; set; }

        public string Content { get; set; }
        public int Hits { get; set; }
        public ContentType Type { get; set; }
        public string FmtType { get; set; }
        public string ThumbImg { get; set; }
        public string Tags { get; set; }
        public string Categories { get; set; }
        public ContentStatus Status { get; set; }
        public int CommentsNum { get; set; }
        public bool AllowComment { get; set; } = true;
        public bool AllowPing { get; set; } = true;
        public bool AllowFeed { get; set; } = true;
        public string Url { get; set; }
        public int AuthorId { get; set; }

        [ForeignKey("AuthorId")]
        public Users Author { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime Created { get; set; }

        public bool Deleted { get; set; }
    }
}