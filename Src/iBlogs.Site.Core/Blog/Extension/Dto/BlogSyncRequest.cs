using System;
using System.Collections.Generic;
using System.Text;

namespace iBlogs.Site.Core.Blog.Extension.Dto
{
    public class BlogSyncRequest
    {
        public int Cid { get; set; }
        public BlogSyncTarget[] Targets { get; set; }
    }
}
