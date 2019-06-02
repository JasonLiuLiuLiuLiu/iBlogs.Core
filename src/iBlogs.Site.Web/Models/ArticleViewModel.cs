using iBlogs.Site.Core.Comment;
using iBlogs.Site.Core.Comment.DTO;
using iBlogs.Site.Core.Common.Response;
using iBlogs.Site.Core.Content.DTO;

namespace iBlogs.Site.Web.Models
{
    public class ArticleViewModel
    {
        public ContentResponse Content { get; set; }
        public Page<CommentResponse> Comments { get; set; }
        public ContentResponse Pre { get; set; }
        public ContentResponse Next { get; set; }

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
