using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iBlogs.Site.Core.Option.DTO
{
    public class OptionParam
    {
        public int Id { get; set; }

        /**
         * 配置键
         */
        public string Key { get; set; }

        /**
         * 配置值
         */
        public string Value { get; set; }

        /**
         * 配置描述
         */
        public string Description { get; set; }

        public bool Editable { get; set; }
    }
}