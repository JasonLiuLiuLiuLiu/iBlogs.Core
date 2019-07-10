using System;
using System.Collections.Generic;
using System.Text;
using iBlogs.Site.Core.Common.Request;

namespace iBlogs.Site.Core.Comment.DTO
{
    public class CommentPageParam : PageParam
    {
        public int? Cid { get; set; }
        public CommentStatus Status { get; set; }
    }
}
