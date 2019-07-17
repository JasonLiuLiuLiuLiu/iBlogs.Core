using System;
using System.Collections.Generic;
using System.Text;

namespace iBlogs.Site.Core.Git
{
    public class MarkDownHeaderContext
    {
        public string Title { get; set; }
        public string Tags { get; set; }
        public string Categories { get; set; }
        public int BlogId { get; set; }
        public DateTime LastUpdate { get; set; }=DateTime.Now;
        public string Message { get; set; } = "以上标签内容请按要求修改";
    }
}
