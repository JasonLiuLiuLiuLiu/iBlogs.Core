using iBlogs.Site.Core.Blog.Content;

namespace iBlogs.Site.Core.Blog.Extension
{
    public class BlogSyncContext
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public BlogSyncMethod Method { get; set; }
        public PostSyncDto Post { get; set; }
        public BlogSyncTarget Target { get; set; }
    }

    public enum BlogSyncMethod
    {
        AddOrUpdate,
        Download,
        Publish,
        Delete
    }

    public class PostSyncDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }
        public string Tags { get; set; }
        public string Categories { get; set; }
        public ContentStatus Status { get; set; }
    }
}
