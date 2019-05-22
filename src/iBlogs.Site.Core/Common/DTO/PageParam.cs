using System;
using System.Collections.Generic;
using System.Text;

namespace iBlogs.Site.Core.Params
{
    public class PageParam
    {
        public int Page { get; set; }
        public int Limit { get; set; } = 20;
    }
}
