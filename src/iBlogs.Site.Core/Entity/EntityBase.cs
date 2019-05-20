using System;
using iBlogs.Site.Core.Utils.Attribute;

namespace iBlogs.Site.Core.Entity
{
    [Serializable]
    public abstract class EntityBase
    {
        public int? Id { get; set; }

        [Column(Name = "author_id")]
        public int? AuthorId { get; set; }

        public long? Created { get; set; }
    }
}
