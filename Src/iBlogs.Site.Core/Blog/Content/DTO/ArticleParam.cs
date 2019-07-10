using iBlogs.Site.Core.Common.Request;

namespace iBlogs.Site.Core.Blog.Content.DTO
{
    public class ArticleParam : PageParam
    {
        public string Title { get; set; }
        public string Categories { get; set; }
        public string Tag { get; set; }
        public ContentStatus? Status { get; set; }
        public ContentType Type { get; set; }
    }
}
