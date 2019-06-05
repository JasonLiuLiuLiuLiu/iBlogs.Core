using System.Collections.Generic;

namespace iBlogs.Site.Core.Meta.DTO
{
    public class TagViewModel
    {
        public int Total { get; set; }
        public List<KeyValuePair<string, int>> Tags { get; set; }
    }
}
