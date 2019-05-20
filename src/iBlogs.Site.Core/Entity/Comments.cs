using System;
using iBlogs.Site.Core.Utils.Attribute;

namespace iBlogs.Site.Core.Entity
{
    public class Comments : EntityBase
    {


        /**
         * comment表主键
         */
        public int Coid { get; set; }

        /**
         * post表主键,关联字段
         */
        public int Cid { get; set; }

        /**
         * 评论作者
         */
        public string Author { get; set; }

        /**
         * 评论所属内容作者id
         */
        [Column(Name = "owner_id")]
        public int OwnerId { get; set; }

        /**
         * 评论者邮件
         */
        public string Mail { get; set; }

        /**
         * 评论者网址
         */
        public string Url { get; set; }

        /**
         * 评论者ip地址
         */
        public string Ip { get; set; }

        /**
         * 评论者客户端
         */
        public string Agent { get; set; }

        /**
         * 评论内容
         */
        public string Content { get; set; }

        /**
         * 评论类型
         */
        public string Type { get; set; }

        /**
         * 评论状态
         */
        public string Status { get; set; }

        /**
         * 父级评论
         */
        public int Parent { get; set; }


    }
}
