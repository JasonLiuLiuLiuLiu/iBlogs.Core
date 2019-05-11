using System;
using System.Collections.Generic;
using System.Text;
using iBlogs.Site.Core.Entity;

namespace iBlogs.Site.Core.Dto
{
    public class Comment : Comments
    {
        public int levels;
        public List<Comments> children;
    }
}
