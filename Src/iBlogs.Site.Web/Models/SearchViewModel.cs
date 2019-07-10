using iBlogs.Site.Core.Blog.Content.DTO;
using iBlogs.Site.Core.Common.Response;

namespace iBlogs.Site.Web.Models
{
    public class SearchViewModel
    {
        public string KeyWord { get; set; }
        public Page<ContentResponse> Contents { get; set; }
    }
}
