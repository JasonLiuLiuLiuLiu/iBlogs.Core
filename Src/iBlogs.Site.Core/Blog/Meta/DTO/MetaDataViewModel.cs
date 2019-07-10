using System.Collections.Generic;

namespace iBlogs.Site.Core.Blog.Meta.DTO
{
    public class MetaDataViewModel
    {
        public int Total { get; set; }
        public List<KeyValuePair<string, int>> Data { get; set; }
    }
}
