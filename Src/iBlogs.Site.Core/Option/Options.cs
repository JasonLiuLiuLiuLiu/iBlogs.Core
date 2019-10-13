using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using iBlogs.Site.Core.Storage;

namespace iBlogs.Site.Core.Option
{
    public class Options : IEntityBase
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 配置键
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 配置值
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 配置描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 是否可编辑,可编辑的配置项将会在系统配置中显示
        /// </summary>
        public bool Editable { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime Created { get; set; }

        public bool Deleted { get; set; }
    }
}