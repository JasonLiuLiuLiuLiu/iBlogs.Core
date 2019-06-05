using System;
using iBlogs.Site.Core.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iBlogs.Site.Core.Option
{
    public class Options : IEntityBase
    {
        [Key]
        public int Id { get; set; }

        /**
         * 配置键
         */
        public string Name { get; set; }

        /**
         * 配置值
         */
        public string Value { get; set; }

        /**
         * 配置描述
         */
        public string Description { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime Created { get; set; }
    }
}