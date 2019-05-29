using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using iBlogs.Site.Core.Content;
using iBlogs.Site.Core.EntityFrameworkCore;
using iBlogs.Site.Core.Meta;

namespace iBlogs.Site.Core.Relationship
{
    public class Relationships:IEntityBase
    {
        [Key]
        public int Id { get; set; }
        /**
         * 文章主键
         */
        public int Cid;
        [ForeignKey("Cid")]
        public Contents Content { get; set; }

        /**
         * 项目主键
         */
        public int Mid;
        [ForeignKey("Mid")]
        public Metas Meta { get; set; }


    }
}
