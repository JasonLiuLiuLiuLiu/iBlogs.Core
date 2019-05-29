using System;
using System.Collections.Generic;
using iBlogs.Site.Core.Comment;

namespace iBlogs.Site.Core.Content.DTO
{
    public class Archive
    {
        public string DateStr { get; set; }
        public DateTime Date { get; set; }
        public string Count { get; set; }
        public List<Contents> Articles { get; set; }
    }
}
