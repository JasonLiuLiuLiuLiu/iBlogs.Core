using System;
using System.Text;
using iBlogs.Site.Core.Common;
using iBlogs.Site.Core.Common.Extensions;
using Markdig;

namespace iBlogs.Site.Core.Content.DTO
{
    [Serializable]
    public class ContentResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public DateTime Modified { get; set; }
        public string Content { get; set; }
        public int Hits { get; set; }
        public string Type { get; set; }
        public string FmtType { get; set; }
        public string ThumbImg { get; set; }
        public string Tags { get; set; }
        public string Categories { get; set; }
        public string Status { get; set; }
        public int CommentsNum { get; set; }
        public bool AllowComment { get; set; } = true;
        public bool AllowPing { get; set; } = true;
        public bool AllowFeed { get; set; } = true;
        public string Author { get; set; }
        public DateTime Created { get; set; }


        /// <summary>
        /// 返回文章链接地址
        /// </summary>
        /// <returns></returns>
        public string Permalink()
        {
            return Permalink(this);
        }

        /// <summary>
        /// 返回文章链接地址
        /// </summary>
        /// <param name="contents"></param>
        /// <returns></returns>

        public string Permalink(ContentResponse contents)
        {
            return Permalink(contents.Id, contents.Slug);
        }

        /// <summary>
        /// 返回文章链接地址
        /// </summary>
        /// <param name="cid"></param>
        /// <param name="slug"></param>
        /// <returns></returns>

        public string Permalink(int cid, string slug)
        {
            return "/article/" + (!slug.IsNullOrWhiteSpace() ? slug : cid.ToString());
        }

        /// <summary>
        /// 显示文章创建日期
        /// </summary>
        /// <param name="frm"></param>
        /// <returns></returns>

        public string CreatedStr(string frm)
        {
            return Created.ToString(frm);
        }

        /// <summary>
        /// 获取文章最后修改时间
        /// </summary>
        /// <param name="fmt"></param>
        /// <returns></returns>

        public string ModifiedStr(string fmt)
        {
            return Modified.ToString(fmt);
        }



        /// <summary>
        /// 显示分类
        /// </summary>
        /// <returns></returns>

        public string ShowCategories()
        {
            return ShowCategories(Categories);
        }

        /// <summary>
        /// 当前文章的分类列表
        /// </summary>
        /// <returns></returns>

        public string[] CategoryList()
        {
            return Categories.Split(',');
        }

        /// <summary>
        /// 当前文章的标签列表
        /// </summary>
        /// <returns></returns>

        public string[] TagList()
        {
            return Tags.Split(',');
        }

        /// <summary>
        /// 显示分类
        /// </summary>
        /// <param name="categories"></param>
        /// <returns></returns>

        public string ShowCategories(string categories)
        {
            if (!categories.IsNullOrWhiteSpace())
            {
                string[] arr = categories.Split(",");
                StringBuilder sbuf = new StringBuilder();
                foreach (var s in arr)
                {
                    sbuf.Append("/ ").Append("<a href=\"/category/" + s + "\">" + s + "</a>");
                }
                return sbuf.Length>0?sbuf.ToString().Substring(1,sbuf.Length-1): sbuf.ToString();
            }
            return ShowCategories("默认分类");
        }

        /// <summary>
        /// 显示标签
        /// </summary>
        /// <param name="split"></param>
        /// <returns></returns>

        public string ShowTags(string split)
        {

            if (stringKit.isNotBlank(Tags))
            {
                string[] arr = Tags.Split(",");
                StringBuilder sbuf = new StringBuilder();
                foreach (string c in arr)
                {
                    sbuf.Append("<a href=\"/tag/" + c + "\">" + c + "</a>");
                }
                return sbuf.ToString();
            }
            return "";
        }

        /// <summary>
        /// 显示标签
        /// </summary>
        /// <returns></returns>

        public string ShowTags()
        {
            return ShowTags(",");
        }

        /// <summary>
        /// 显示文章内容，格式化markdown后的
        /// </summary>
        /// <returns></returns>

        public string ShowContent()
        {
            return Article(Content);
        }

        /// <summary>
        /// 获取文章摘要
        /// </summary>
        /// <returns></returns>

        public string Intro()
        {
            return Intro(Content, iBlogsConfig.MAX_INTRO_COUNT);
        }

        /// <summary>
        /// 截取文章摘要(返回HTML)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>

        public string Intro(string value)
        {
            if (stringKit.isBlank(value))
            {
                return null;
            }
            int pos = value.IndexOf("<!--more-->", StringComparison.Ordinal);
            if (pos != -1)
            {
                string html = value.Substring(0, pos);
                return Markdown.ToHtml(html);
            }
            else
            {
                return Markdown.ToHtml(value);
            }
        }

        /// <summary>
        /// 截取文章摘要
        /// </summary>
        /// <param name="value"></param>
        /// <param name="len"></param>
        /// <returns></returns>

        public string Intro(string value, int len)
        {
            int pos = value.IndexOf("<!--more-->", StringComparison.Ordinal);
            if (pos != -1)
            {
                string html = value.Substring(0, pos);
                return BlogsUtils.HtmlToText(Markdown.ToHtml(html));
            }
            else
            {
                string text = BlogsUtils.HtmlToText(Markdown.ToHtml(value));
                if (text.Length > len)
                {
                    return text.Substring(0, len);
                }
                return text;
            }
        }

        /// <summary>
        /// 显示文章内容，转换markdown为html
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>

        public string Article(string value)
        {
            if (stringKit.isNotBlank(value))
            {
                value = value.Replace("<!--more-->", "\r\n");
                return Markdown.ToHtml(value);
            }
            return "";
        }

        /// <summary>
        /// 显示文章缩略图，顺序为：文章第一张图 -> 随机获取
        /// </summary>
        /// <returns></returns>

        public string ShowThumb()
        {

            if (stringKit.isNotBlank(ThumbImg))
            {
                string newFileName = BlogsUtils.GetFileName(ThumbImg);
                string thumbnailImgUrl = (ThumbImg).Replace(newFileName, "thumbnail_" + newFileName);
                return thumbnailImgUrl;
            }
            string content = Article(Content);
            string img = BlogsUtils.ShowThumb(content);
            if (stringKit.isNotBlank(img))
            {
                return img;
            }
            int? cid = Id;
            int? size = cid % 20;
            size = size == 0 ? 1 : size;
            return "/templates/themes/default/static/img/rand/" + size + ".jpg";
        }

        private string[] ICONS = { "bg-ico-book", "bg-ico-game", "bg-ico-note", "bg-ico-chat", "bg-ico-SetCode", "bg-ico-image", "bg-ico-web", "bg-ico-link", "bg-ico-design", "bg-ico-lock" };

        /**
         * 显示文章图标
         *
         * @return
         */

        public string ShowIcon()
        {
            return ICONS[Id % ICONS.Length];
        }

        /**
        * 显示评论
        *
        * @param noComment 评论为0的时候显示的文本
        * @param value     评论组装文本
        * @return
        */

        public string CommentsNumStr(string noComment, string value)
        {
            return CommentsNum > 0 ? string.Format(value, CommentsNum) : noComment;
        }
    }
}