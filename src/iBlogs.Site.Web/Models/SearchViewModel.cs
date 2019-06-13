using iBlogs.Site.Core.Common.Response;
using iBlogs.Site.Core.Content.DTO;

namespace iBlogs.Site.Web.Models
{
    public class SearchViewModel
    {
        public string KeyWord { get; set; }
        public Page<ContentResponse> Contents { get; set; }
    }
}
