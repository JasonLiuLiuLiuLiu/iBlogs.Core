using System;
using System.Collections.Generic;
using System.Text;

namespace iBlogs.Site.Core.Entity
{
    public class Comments:EntityBase
    {


        /**
         * comment表主键
         */
        public int Coid{get;set;}

        /**
         * post表主键,关联字段
         */
        public int Cid{get;set;}

        /**
         * 评论作者
         */
        public String Author{get;set;}

        /**
         * 评论所属内容作者id
         */
        public int OwnerId{get;set;}

        /**
         * 评论者邮件
         */
        public String Mail{get;set;}

        /**
         * 评论者网址
         */
        public String Url{get;set;}

        /**
         * 评论者ip地址
         */
        public String Ip{get;set;}

        /**
         * 评论者客户端
         */
        public String Agent{get;set;}

        /**
         * 评论内容
         */
        public String Content{get;set;}

        /**
         * 评论类型
         */
        public String Type{get;set;}

        /**
         * 评论状态
         */
        public String Status{get;set;}

        /**
         * 父级评论
         */
        public int Parent{get;set;}


    }
}
