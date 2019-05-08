using System;
using System.Collections.Generic;
using System.Text;
using iBlogs.Site.Core.Entity;

namespace iBlogs.Site.Core.Response
{
    public class Archive
    {
        public String DateStr { get; set; }
        public DateTime Date { get; set; }
        public String Count { get; set; }
        public List<Contents> Articles { get; set; }
    }
}
