using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using iBlogs.Site.Core.User;

namespace iBlogs.Site.Core.Attach
{
    public class Attachment
    {
        [Key]
        public int Id { get; set; }
        public int AuthorId { get; set; }
        [ForeignKey("AuthorId")]
        public Users Author { get; set; }
        public long Created { get; set; }
        public string FName { get; set; }
        public string FType { get; set; }
        public string FKey { get; set; }
    }
}
