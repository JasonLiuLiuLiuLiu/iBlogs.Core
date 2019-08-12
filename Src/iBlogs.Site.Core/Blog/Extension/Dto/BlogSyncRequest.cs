namespace iBlogs.Site.Core.Blog.Extension.Dto
{
    public class BlogSyncRequest
    {
        public int Cid { get; set; }
        public BlogSyncMethod Method { get; set; }
        public BlogSyncTarget[] Targets { get; set; }
    }
}
