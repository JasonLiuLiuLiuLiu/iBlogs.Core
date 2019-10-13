using iBlogs.Site.Core.Blog.Attach;
using iBlogs.Site.Core.Blog.Comment;
using iBlogs.Site.Core.Blog.Content;
using iBlogs.Site.Core.Blog.Extension;
using iBlogs.Site.Core.Blog.Meta;
using iBlogs.Site.Core.Blog.Relationship;
using iBlogs.Site.Core.Option;
using iBlogs.Site.Core.Security;
using Microsoft.EntityFrameworkCore;

namespace iBlogs.Site.Core.EntityFrameworkCore
{
    public class BlogsContext : DbContext
    {
        public BlogsContext(DbContextOptions<BlogsContext> options) : base(options)
        {
        }

        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<Comments> Comments { get; set; }
        public DbSet<Contents> Contents { get; set; }
        public DbSet<Metas> Metas { get; set; }
        public DbSet<Options> Options { get; set; }
        public DbSet<Relationships> Relationships { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<BlogSyncRelationship> BlogSyncRelationships { get; set; }
    }
}