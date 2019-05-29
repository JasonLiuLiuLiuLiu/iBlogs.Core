using iBlogs.Site.Core.Attach;
using iBlogs.Site.Core.Comment;
using iBlogs.Site.Core.Content;
using iBlogs.Site.Core.Log;
using iBlogs.Site.Core.Meta;
using iBlogs.Site.Core.Option;
using iBlogs.Site.Core.Relationship;
using iBlogs.Site.Core.User;
using Microsoft.EntityFrameworkCore;

namespace iBlogs.Site.Core.EntityFrameworkCore
{
    public class iBlogsContext:DbContext
    {
        public iBlogsContext(DbContextOptions<iBlogsContext> options) : base(options)
        {

        }

        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<Comments> Comments { get; set; }
        public DbSet<Contents> Contents { get; set; }
        public DbSet<Logs> Logs { get; set; }
        public DbSet<Metas> Metas { get;set;}
        public DbSet<Options> Options { get; set; }
        public DbSet<Relationships> Relationships { get; set; }
        public DbSet<Users> Users { get; set; }

    }
}
