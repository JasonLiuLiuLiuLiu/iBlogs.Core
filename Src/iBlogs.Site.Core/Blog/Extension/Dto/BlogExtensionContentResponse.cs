using System;

namespace iBlogs.Site.Core.Blog.Extension.Dto
{
    public class BlogExtensionContentResponse
    {
        public int Id { get; set; }
        public int ContentId { get; set; }
        public string ContentTitle { get; set; }
        public BlogSyncTarget Target { get; set; }
        public string TargetPostId { get; set; }
        public DateTime SyncData { get; set; }
        public bool Successful { get; set; }
        public string Message { get; set; }
        public string ExtensionProperty { get; set; }
    }
}
