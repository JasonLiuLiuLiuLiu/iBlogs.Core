using iBlogs.Site.Core.Common.Response;
using iBlogs.Site.Core.Content.DTO;

namespace iBlogs.Site.Web.Models
{
    public class IndexViewModel
    {
        public string DisplayType { get; set; }
        public string DisplayMeta { get; set; }
        public string OrderType { get; set; }
        public Page<ContentResponse> Contents { get; set; }
    }
}
