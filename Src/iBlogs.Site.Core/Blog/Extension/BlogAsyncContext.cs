using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using iBlogs.Site.Core.Blog.Content;
using iBlogs.Site.Core.Security;

namespace iBlogs.Site.Core.Blog.Extension
{
    public class BlogAsyncContext
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public BlogSyncMethod Method { get; set; }
        public PostSyncDto Post { get; set; }
    }

    public enum BlogSyncMethod
    {
        AddOrUpdate,
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
