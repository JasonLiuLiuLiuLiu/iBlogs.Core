using System.Collections.Generic;

namespace iBlogs.Site.Core.Blog.Content.DTO
{
    public class Archive
    {
        public string DateStr { get; set; }
        public int Count { get; set; }
        public IEnumerable<ContentResponse> Contents { get; set; }
    }

    public class ArchiveEntity
    {
        public string DateStr { get; set; }
        public int Count { get; set; }
        public IEnumerable<Contents> Contents { get; set; }
    }
}