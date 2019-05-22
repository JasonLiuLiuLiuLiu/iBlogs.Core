using System;
using iBlogs.Site.Core.Common.Attribute;

namespace iBlogs.Site.Core.Comment
{
    [Serializable]
    public class Contents : EntityBase
    {

        /**
         * 文章表主键
         */
        public int? Cid { get; set; }

        /**
         * 文章标题
         */
        public string Title { get; set; }

        /**
         * 文章缩略名
         */
        public string Slug { get; set; }

        /**
         * 文章修改时间戳
         */
        public int? Modified { get; set; }

        /**
         * 文章内容
         */
        public string Content { get; set; }

        /**
         * 文章点击次数
         */
        public int? Hits { get; set; }

        /**
         * 文章类型： PAGE、POST
         */
        public string Type { get; set; }

        /**
         * 内容类型，markdown或者html
         */
        [Column(Name = "fmt_type")]
        public string FmtType { get; set; }

        /**
         * 文章缩略图
         */
        [Column(Name = "thumb_img")]
        public string ThumbImg { get; set; }

        /**
         * 标签列表
         */
        public string Tags { get; set; }

        /**
         * 分类列表
         */
        public string Categories { get; set; }

        /**
         * 内容状态
         */
        public string Status { get; set; }

        /**
         * 内容所属评论数
         */
        [Column(Name = "comments_num")]
        public int? CommentsNum { get; set; }

        /**
         * 是否允许评论
         */
        [Column(Name = "allow_comment")]
        public bool AllowComment { get; set; }=true;

        /**
         * 是否允许ping
         */
        [Column(Name = "allow_ping")]
        public bool AllowPing { get; set; }

        /**
         * 允许出现在Feed中
         */
        [Column(Name = "allow_feed")]
        public bool AllowFeed { get; set; }

        public string Url { get; set; }
    }
}
