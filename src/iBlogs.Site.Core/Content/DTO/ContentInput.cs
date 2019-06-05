using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace iBlogs.Site.Core.Content.DTO
{
    public class ContentInput
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime Modified { get; set; }

        public string Content { get; set; }
        public int Hits { get; set; }
        public string Type { get; set; }
        public string FmtType { get; set; }
        public string ThumbImg { get; set; }
        public string Tags { get; set; }
        public string Categories { get; set; }
        public string Status { get; set; }
        public int CommentsNum { get; set; }
        public bool AllowComment { get; set; } = true;
        public bool AllowPing { get; set; } = true;
        public bool AllowFeed { get; set; } = true;
        public string Url { get; set; }
        public int AuthorId { get; set; }
        public DateTime Created { get; set; }
    }
}