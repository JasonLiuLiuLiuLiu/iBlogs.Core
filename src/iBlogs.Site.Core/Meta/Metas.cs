using System.ComponentModel.DataAnnotations;
using iBlogs.Site.Core.EntityFrameworkCore;

namespace iBlogs.Site.Core.Meta
{
    public class Metas:IEntityBase
    {

       
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public int Sort { get; set; }
        public int Parent { get; set; }
        public int Count { get; set; }
        [Timestamp]
        public byte[] Timestamp { get; set; }
    }
}
