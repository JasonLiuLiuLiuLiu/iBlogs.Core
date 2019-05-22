using iBlogs.Site.Core.Common.DTO;

namespace iBlogs.Site.Core.Content.DTO
{
    public class ArticleParam : PageParam
    {

        public string Title{get;set;}
        public string Categories{get;set;}
        public string Status{get;set;}
        public string Type { get; set; } = "post";
        public string OrderBy { get; set; } = "created";
        public string OrderType { get; set; } = " desc ";

    }
}
