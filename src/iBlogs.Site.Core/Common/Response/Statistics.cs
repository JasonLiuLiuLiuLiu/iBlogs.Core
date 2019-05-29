namespace iBlogs.Site.Core.Common.Response
{
    public class Statistics
    {
        /**
         * 文章数
         */
        public long Articles { get; set; }

        /**
         * 页面数
         */
        public long Pages { get; set; }
        /**
         * 评论数
         */
        public long Comments { get; set; }
        /**
         * 分类数
         */
        public long Categories { get; set; }
        /**
         * 标签数
         */
        public long Tags { get; set; }
        /**
         * 附件数
         */
        public long Attachs { get; set; }

    }
}
