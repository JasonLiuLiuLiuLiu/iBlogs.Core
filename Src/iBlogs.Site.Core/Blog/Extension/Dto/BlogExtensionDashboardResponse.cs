namespace iBlogs.Site.Core.Blog.Extension.Dto
{
    public  class BlogExtensionDashboardResponse
    {
        public BlogExtensionContentResponse[] Successful { get; set; }
        public BlogExtensionContentResponse[] Failed { get; set; }
    }
}
