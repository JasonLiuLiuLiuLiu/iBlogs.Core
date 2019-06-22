using System;
using iBlogs.Site.Core.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iBlogs.Site.Core.Meta
{
    public class Metas : IEntityBase
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Slug { get; set; }
        public MetaType Type { get; set; }
        public string Description { get; set; }
        public int Sort { get; set; }
        public int Parent { get; set; }
        public int Count { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime Created { get; set; }

        public bool Deleted { get; set; }
    }
}