using iBlogs.Site.Core.Blog.Comment.DTO;
using iBlogs.Site.Core.Blog.Content.DTO;
using iBlogs.Site.Core.Common.Response;
using iBlogs.Site.Core.Security.DTO;

namespace iBlogs.Site.Web.Models
{
    public class ArticleViewModel
    {
        public ContentResponse Content { get; set; }
        public Page<CommentResponse> Comments { get; set; }
        public ContentResponse Pre { get; set; }
        public ContentResponse Next { get; set; }
        public CurrentUser User { get; set; }

        /**
        * 当前文章的下一篇文章链接
        *
        * @param title 文章标题
        * @return
        */

        public string TheNext(string title)
        {
            if (null != Next)
            {
                return "<a href=\"" + Next.Permalink() + "\" title=\"" + Next.Title + "\">" + title + "</a>";
            }
            return "";

        }

        /**
         * 当前文章的下一篇文章链接
         *
         * @param title 文章标题
         * @return
         */

        public string ThePrev(string title)
        {
            if (null != Pre)
            {
                return "<a href=\"" + Pre.Permalink() + "\" title=\"" + Pre.Title + "\">" + title + "</a>";
            }
            return "";

        }
    }
}
