using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using iBlogs.Site.Core.Security;
using iBlogs.Site.Core.Storage;

namespace iBlogs.Site.Core.Blog.Attach
{
    public class Attachment : IEntityBase
    {
        [Key]
        public int Id { get; set; }

        public int AuthorId { get; set; }

        [ForeignKey("AuthorId")]
        public Users Author { get; set; }
        public string FName { get; set; }
        public string FType { get; set; }
        public string FKey { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime Created { get; set; }

        public bool Deleted { get; set; }
    }
}