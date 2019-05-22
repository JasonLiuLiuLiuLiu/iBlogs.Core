using System.ComponentModel.DataAnnotations;

namespace iBlogs.Site.Core.Meta
{
    public class Metas
    {

        /**
         * 项目主键
         */
        [Key]
        public int Mid { get; set; }

        /**
         * 项目名称
         */
        public string Name { get; set; }

        /**
         * 项目缩略名
         */
        public string Slug { get; set; }

        /**
         * 项目类型
         */
        public string Type { get; set; }

        /**
         * 项目描述
         */
        public string Description { get; set; }

        /**
         * 项目排序
         */
        public int Sort { get; set; }

        /**
         * 父级
         */
        public int Parent { get; set; }

        /**
         * 项目下文章数
         */
        public int Count { get; set; }
        [Timestamp]
        public byte[] Timestamp { get; set; }
    }
}
