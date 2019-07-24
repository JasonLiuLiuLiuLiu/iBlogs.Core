using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using iBlogs.Site.Core.Blog.Content;
using iBlogs.Site.Core.EntityFrameworkCore;

namespace iBlogs.Site.Core.Blog.Extension
{
   public class BlogSyncRelationship:IEntityBase
    {
        [Key]
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public bool Deleted { get; set; }

        public int ContentId { get; set; }
        [ForeignKey("ContentId")]
        public Contents Content { get; set; }

        public BlogSyncTarget Target { get; set; }

        public string TargetPostId { get; set; }
        public DateTime SyncData { get; set; }
        public string Message { get; set; }
        public string ExtensionProperty { get; set; }
    }

   public enum AsyncTarget
   {
       CnBlogs
   }
}
