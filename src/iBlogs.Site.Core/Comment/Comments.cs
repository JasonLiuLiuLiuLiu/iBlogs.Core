using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using iBlogs.Site.Core.Content;
using iBlogs.Site.Core.User;

namespace iBlogs.Site.Core.Comment
{
    public class Comments
    {

        /**
        * comment表主键
        */
        [Key]
        public int Coid { get; set; }
        public int? AuthorId { get; set; }
        [ForeignKey("AuthorId")]
        public Users User { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime Created { get; set; }
        /**
         * post表主键,关联字段
         */
        public int Cid { get; set; }
        [ForeignKey("Cid")]
        public Contents Article { get; set; }

        /**
         * 评论作者
         */
        public string Author { get; set; }

        /**
         * 评论所属内容作者id
         */
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
