using iBlogs.Site.Core.Common.DTO;
using iBlogs.Site.Core.Common.Request;

namespace iBlogs.Site.Core.Content.DTO
{
    public class ArticleParam : PageParam
    {
        public string Title { get; set; }
        public string Categories { get; set; }
        public string Tag { get; set; }
        public string Status { get; set; }
        public string Type { get; set; } = "post";
    }
}
