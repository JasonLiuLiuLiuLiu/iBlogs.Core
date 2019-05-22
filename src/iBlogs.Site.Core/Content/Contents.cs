﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using iBlogs.Site.Core.Common.Extensions;
using iBlogs.Site.Core.User;

namespace iBlogs.Site.Core.Content
{
    [Serializable]
    public class Contents 
    {
        /**
         * 文章表主键
         */
        [Key]
        public int Cid { get; set; }

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
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime Modified { get; set; }

        /**
         * 文章内容
         */
        public string Content { get; set; }

        /**
         * 文章点击次数
         */
        public int Hits { get; set; }

        /**
         * 文章类型： PAGE、POST
         */
        public string Type { get; set; }

        /**
         * 内容类型，markdown或者html
         */
        public string FmtType { get; set; }

        /**
         * 文章缩略图
         */
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
        public int CommentsNum { get; set; }

        /**
         * 是否允许评论
         */
        public bool AllowComment { get; set; }=true;

        /**
         * 是否允许ping
         */
        public bool AllowPing { get; set; }

        /**
         * 允许出现在Feed中
         */
        public bool AllowFeed { get; set; }

        public string Url { get; set; }

        public int AuthorId { get; set; }
        [ForeignKey("AuthorId")]
        public Users Author { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime Created { get; set; }
    }
}
