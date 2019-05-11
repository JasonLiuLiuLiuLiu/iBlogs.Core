using System;
using System.Collections.Generic;
using System.Text;
using iBlogs.Site.Core.Entity;
using iBlogs.Site.Core.Extensions;
using iBlogs.Site.Core.Response;
using iBlogs.Site.Core.Service.Options;
using iBlogs.Site.Core.Utils;
using Markdig;
using Markdig.Extensions.Emoji;
using Microsoft.Extensions.FileSystemGlobbing;

namespace iBlogs.Site.Core.Service.Common
{
    public class ViewService : IViewService
    {
        private readonly IOptionService _optionService;
        private readonly ISiteService siteService;
        private Contents _currentArticle;

        public ViewService(IOptionService optionService, ISiteService siteService)
        {
            _optionService = optionService;
            this.siteService = siteService;
        }

        public bool is_post { get; set; }

        public void Set_current_article(Contents contents)
        {
            _currentArticle = contents;
        }


        public Contents current_article()
        {
            return _currentArticle;
        }

        /**
         * 获取header keywords
         *
         * @return
         */
        public string meta_keywords()
        {

            var value = _optionService.Get("keywords");
            if (null != value)
            {
                return value;
            }
            return _optionService.Get("site_keywords");
        }

        /**
         * 获取header description
         *
         * @return
         */
        public string meta_description()
        {

            var value = _optionService.Get("description");
            if (null != value)
            {
                return value;
            }
            return _optionService.Get("site_description");
        }

        /**
         * header title
         *
         * @return
         */
        public string head_title()
        {

            var value = _optionService.Get("title");

            string p = "";
            if (null != value)
            {
                p = value;
            }
            return p + _optionService.Get("site_title", "iBlogs");
        }

        public string site_title()
        {
            return _optionService.Get("site_title", "iBlogs");
        }

        public string site_option(string key)
        {
            return _optionService.Get(key);
        }

        /**
         * 返回文章链接地址
         *
         * @return
         */
        public string permalink()
        {
            var contents = current_article();
            return null != contents ? permalink(contents) : "";
        }

        /**
         * 返回文章链接地址
         *
         * @param contents
         * @return
         */
        public string permalink(Contents contents)
        {
            return permalink(contents.Cid, contents.Slug);
        }

        /**
         * 返回文章链接地址
         *
         * @param cid
         * @param slug
         * @return
         */
        public string permalink(int cid, string slug)
        {
            return "/article/" + (!slug.IsNullOrWhiteSpace() ? slug : cid.ToString());
        }

        /**
         * 显示文章创建日期
         *
         * @param fmt
         * @return
         */
        public string created(string frm)
        {
            Contents contents = current_article();
            return contents?.Created.ToString(frm);

        }

        /**
         * 获取文章最后修改时间
         *
         * @param fmt
         * @return
         */
        public string modified(string fmt)
        {
            Contents contents = current_article();
            return contents?.Modified.ToString();

        }

        /**
         * 返回文章浏览数
         *
         * @return
         */
        public int hits()
        {
            Contents contents = current_article();
            return contents?.Hits ?? 0;
        }

        /**
         * 显示分类
         *
         * @return
         */
        public string show_categories()
        {
            Contents contents = current_article();
            if (null != contents)
            {
                return show_categories(contents.Categories);
            }
            return "";
        }

        /**
         * 当前文章的分类列表
         *
         * @return
         * @since b1.3.0
         */
        public string[] category_list()
        {
            Contents contents = current_article();
            if (null != contents && !contents.Categories.IsNullOrWhiteSpace())
            {
                return contents.Categories.Split(',');
            }
            return null;
        }

        /**
         * 当前文章的标签列表
         *
         * @return
         * @since b1.3.0
         */
        public string[] tag_list()
        {
            Contents contents = current_article();
            if (null != contents && !contents.Tags.IsNullOrWhiteSpace())
            {
                return contents.Tags.Split(',');
            }
            return null;
        }

        /**
         * 显示分类
         *
         * @param categories
         * @return
         */
        public string show_categories(string categories)
        {
            if (!categories.IsNullOrWhiteSpace())
            {
                string[] arr = categories.Split(",");
                StringBuilder sbuf = new StringBuilder();
                foreach (var s in arr)
                {
                    sbuf.Append("<a href=\"/category/" + s + "\">" + s + "</a>");
                }
                return sbuf.ToString();
            }
            return show_categories("默认分类");
        }

        /**
         * 显示标签
         *
         * @param split 每个标签之间的分隔符
         * @return
         */
        public string show_tags(string split)
        {
            Contents contents = current_article();
            if (stringKit.isNotBlank(contents.Tags))
            {
                string[] arr = contents.Tags.Split(",");
                StringBuilder sbuf = new StringBuilder();
                foreach (string c in arr)
                {
                    sbuf.Append(split).Append("<a href=\"/tag/" + c + "\">" + c + "</a>");
                }
                return sbuf.Length > 0 ? sbuf.ToString().Substring(0, split.Length - 1) : sbuf.ToString();
            }
            return "";
        }

        /**
         * 显示文章浏览量
         *
         * @return
         */
        public string views()
        {
            Contents contents = current_article();
            return null != contents ? contents.Hits.ToString() : "0";
        }

        /**
         * 显示标签
         *
         * @return
         */
        public string show_tags()
        {
            return show_tags("");
        }

        /**
         * 显示文章内容，格式化markdown后的
         *
         * @return
         */
        public string show_content()
        {
            Contents contents = current_article();
            return null != contents ? article(contents.Content) : "";
        }

        /**
         * 获取文章摘要
         *
         * @param len
         * @return
         */
        public string excerpt(int len)
        {
            return intro(len);
        }

        /**
         * 获取文章摘要
         *
         * @param len
         * @return
         */
        public string intro(int len)
        {
            Contents contents = current_article();
            if (null != contents)
            {
                return intro(contents.Content, len);
            }
            return "";
        }

        /**
         * 截取文章摘要(返回HTML)
         *
         * @param value 文章内容
         * @return 转换 markdown 为 html
         */
        public string intro(string value)
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

        /**
         * 截取文章摘要
         *
         * @param value 文章内容
         * @param len   要截取文字的个数
         * @return
         */
        public string intro(string value, int len)
        {
            int pos = value.IndexOf("<!--more-->");
            if (pos != -1)
            {
                string html = value.Substring(0, pos);
                return IBlogsUtils.htmlToText(Markdown.ToHtml(html));
            }
            else
            {
                string text = IBlogsUtils.htmlToText(Markdown.ToHtml(value));
                if (text.Length > len)
                {
                    return text.Substring(0, len);
                }
                return text;
            }
        }

        /**
         * 显示文章内容，转换markdown为html
         *
         * @param value
         * @return
         */
        public string article(string value)
        {
            if (stringKit.isNotBlank(value))
            {
                value = value.Replace("<!--more-->", "\r\n");
                return Markdown.ToHtml(value);
            }
            return "";
        }

        /**
         * 显示文章缩略图，顺序为：文章第一张图 -> 随机获取
         *
         * @return
         */
        public string show_thumb(Contents contents)
        {
            if (null == contents)
            {
                return "";
            }
            Set_current_article(contents);
            if (stringKit.isNotBlank(contents.ThumbImg))
            {
                string newFileName = IBlogsUtils.getFileName(contents.ThumbImg);
                string thumbnailImgUrl = (contents.ThumbImg).Replace(newFileName, "thumbnail_" + newFileName);
                return thumbnailImgUrl;
            }
            string content = article(contents.Content);
            string img = IBlogsUtils.show_thumb(content);
            if (stringKit.isNotBlank(img))
            {
                return img;
            }
            int cid = contents.Cid;
            int size = cid % 20;
            size = size == 0 ? 1 : size;
            return "/templates/themes/default/static/img/rand/" + size + ".jpg";
        }

        /**
         * 获取当前文章的下一篇
         *
         * @return
         */
        public Contents article_next()
        {
            Contents cur = current_article();
            return null != cur ? siteService.getNhContent(Types.NEXT, cur.Created) : null;
        }

        /**
         * 获取当前文章的上一篇
         *
         * @return
         */
        public Contents article_prev()
        {
            Contents cur = current_article();
            return null != cur ? siteService.getNhContent(Types.PREV, cur.Created) : null;
        }

        /**
         * 当前文章的下一篇文章链接
         *
         * @return
         */
        public string theNext()
        {
            Contents contents = article_next();
            if (null != contents)
            {
                return theNext(Title(contents));
            }
            return "";
        }

        /**
         * 当前文章的下一篇文章链接
         *
         * @param title 文章标题
         * @return
         */
        public string theNext(string title)
        {
            Contents contents = article_next();
            if (null != contents)
            {
                return "<a href=\"" + permalink(contents) + "\" title=\"" + Title(contents) + "\">" + title + "</a>";
            }
            return "";
        }

        /**
         * 当前文章的下一篇文章链接
         *
         * @return
         */
        public string thePrev()
        {
            Contents contents = article_prev();
            if (null != contents)
            {
                return thePrev(Title(contents));
            }
            return "";
        }

        /**
         * 当前文章的下一篇文章链接
         *
         * @param title 文章标题
         * @return
         */
        public string thePrev(string title)
        {
            Contents contents = article_prev();
            if (null != contents)
            {
                return "<a href=\"" + permalink(contents) + "\" title=\"" + Title(contents) + "\">" + title + "</a>";
            }
            return "";
        }

        /**
         * 最新文章
         *
         * @param limit
         * @return
         */
        public List<Contents> recent_articles(int limit)
        {
            if (null == siteService)
            {
                return new List<Contents>();
            }
            return siteService.getContens(Types.RECENT_ARTICLE, limit);
        }

        /**
         * 随机获取文章
         *
         * @param limit
         * @return
         */
        public List<Contents> rand_articles(int limit)
        {
            if (null == siteService)
            {
                return new List<Contents>();
            }
            return siteService.getContens(Types.RANDOM_ARTICLE, limit);
        }

        /**
         * 最新评论
         *
         * @param limit
         * @return
         */
        public List<Comments> recent_comments(int limit)
        {
            if (null == siteService)
            {
                return new List<Comments>();
            }
            return siteService.recentComments(limit);
        }

        /**
         * 获取分类列表
         *
         * @return
         */
        public List<Metas> categories(int limit)
        {
            if (null == siteService)
            {
                return new List<Metas>();
            }
            return siteService.getMetas(Types.RECENT_META, Types.CATEGORY, limit);
        }

        /**
         * 随机获取limit个分类
         *
         * @param limit
         * @return
         */
        public List<Metas> rand_categories(int limit)
        {
            if (null == siteService)
            {
                return new List<Metas>();
            }
            return siteService.getMetas(Types.RANDOM_META, Types.CATEGORY, limit);
        }

        /**
         * 获取所有分类
         *
         * @return
         */
        public List<Metas> categories()
        {
            return categories(iBlogsConst.MAX_POSTS);
        }

        /**
         * 获取标签列表
         *
         * @return
         */
        public List<Metas> tags(int limit)
        {
            if (null == siteService)
            {
                return new List<Metas>();
            }
            return siteService.getMetas(Types.RECENT_META, Types.TAG, limit);
        }

        /**
         * 随机获取limit个标签
         *
         * @param limit
         * @return
         */
        public List<Metas> rand_tags(int limit)
        {
            if (null == siteService)
            {
                return new List<Metas>();
            }
            return siteService.getMetas(Types.RANDOM_META, Types.TAG, limit);
        }

        /**
         * 获取所有标签
         *
         * @return
         */
        public List<Metas> tags()
        {
            return tags(iBlogsConst.MAX_POSTS);
        }

        /**
         * 获取评论at信息
         *
         * @param coid
         * @return
         */
        public string comment_at(int coid)
        {
            if (null == siteService)
            {
                return "";
            }
            Comments comments = siteService.getComment(coid);
            if (null != comments)
            {
                return "<a href=\"#comment-" + coid + "\">@" + comments.Author + "</a>";
            }
            return "";
        }

        private string[] ICONS = { "bg-ico-book", "bg-ico-game", "bg-ico-note", "bg-ico-chat", "bg-ico-code", "bg-ico-image", "bg-ico-web", "bg-ico-link", "bg-ico-design", "bg-ico-lock" };

        /**
         * 显示文章图标
         *
         * @return
         */
        public string show_icon()
        {
            Contents contents = current_article();
            if (null != contents)
            {
                return show_icon(contents.Cid);
            }
            return show_icon(1);
        }

        /**
         * 显示文章图标
         *
         * @param cid
         * @return
         */
        public string show_icon(int cid)
        {
            return ICONS[cid % ICONS.Length];
        }

        /**
         * 显示文章标题
         *
         * @return
         */
        public string Title()
        {
            return Title(current_article());
        }

        /**
         * 返回文章标题
         *
         * @param contents
         * @return
         */
        public string Title(Contents contents)
        {
            return null != contents ? contents.Title : site_title();
        }

        /**
         * 返回社交账号链接
         *
         * @param type
         * @return
         */
        public string social_link(string type)
        {
            string id = site_option("social_" + type);
            switch (type)
            {
                case "github":
                    return "https://github.com/" + id;
                case "weibo":
                    return "http://weibo.com/" + id;
                case "twitter":
                    return "https://twitter.com/" + id;
                case "zhihu":
                    return "https://www.zhihu.com/people/" + id;
                default:
                    return null;
            }
        }

        /**
         * 获取当前文章/页面的评论
         *
         * @param limit
         * @return
         */
        public Page<Dto.Comment> comments(int limit)
        {
            //Contents contents = current_article();
            //if (null == contents)
            //{
            //    return new Page<>();
            //}
            //InterpretContext ctx = InterpretContext.current();
            //var value = ctx.getValueStack().getValue("cp");
            //int page = 1;
            //if (null != value)
            //{
            //    page = (int)value;
            //}
            //Page<Comments> comments = siteService.getComments(contents.Cid, page, limit);
            //return comments;
            return new Page<Dto.Comment>();
        }

        /**
         * 获取当前文章/页面的评论数量
         *
         * @return 当前页面的评论数量
         */
        public long commentsCount()
        {
            Contents contents = current_article();
            if (null == contents)
            {
                return 0;
            }
            return siteService.getCommentCount(contents.Cid);
        }

        /**
         * 分页
         *
         * @param limit
         * @return
         */
        public Page<Contents> articles(int limit)
        {
            return null;
        }


        /**
         * 显示评论
         *
         * @param noComment 评论为0的时候显示的文本
         * @param value     评论组装文本
         * @return
         */
        public string comments_num(string noComment, string value)
        {
            Contents contents = current_article();
            if (null == contents)
            {
                return noComment;
            }
            return contents.CommentsNum > 0 ? string.Format(value, contents.CommentsNum) : noComment;
        }

        /**
         * 返回主题设置选项
         *
         * @param key
         * @return
         */
        public string theme_option(string key, string defaultValue)
        {
            string option = theme_option(key);
            if (stringKit.isBlank(option))
            {
                return defaultValue;
            }
            return option;
        }

        /**
         * 返回主题设置选项
         *
         * @param key
         * @return
         */
        public string theme_option(string key)
        {
            return null;
        }

        /**
         * 返回是否是某个页面
         *
         * @param pageName
         * @return
         */
        public bool is_slug(string pageName)
        {
            return false;
        }




        /**
         * 判断字符串不为空
         *
         * @param str
         * @return
         */
        public bool not_empty(String str)
        {
            return stringKit.isNotBlank(str);
        }

        /**
         * 返回网站首页链接，如：http://tale.biezhi.me
         *
         * @return
         */
        public String site_url()
        {
            return site_url("");
        }

        /**
         * 返回当前主题名称
         *
         * @return
         */
        public String site_theme()
        {
            return site_option("site_theme", "default");
        }

        /**
         * 返回网站链接下的全址
         *
         * @param sub 后面追加的地址
         * @return
         */
        public String site_url(String sub)
        {
            return site_option("site_url") + sub;
        }


        /**
         * 网站子标题
         *
         * @return
         */
        public String site_subtitle()
        {
            return site_option("site_subtitle");
        }

        /**
         * 是否允许使用云公共静态资源
         *
         * @return
         */
        public String allow_cloud_CDN()
        {
            return site_option("allow_cloud_CDN");
        }


        /**
         * 网站配置项
         *
         * @param key
         * @param defalutValue 默认值
         * @return
         */
        public string site_option(String key, String defalutValue)
        {
            if (stringKit.isBlank(key))
            {
                return "";
            }
            return _optionService.Get(key, defalutValue);
        }

        /**
         * 返回站点设置的描述信息
         *
         * @return
         */
        public String site_description()
        {
            return site_option("site_description");
        }

        /**
         * 截取字符串
         *
         * @param str
         * @param len
         * @return
         */
        public String substr(String str, int len)
        {
            if (str.Length > len)
            {
                return str.Substring(0, len);
            }
            return str;
        }

        /**
         * 返回主题URL
         *
         * @return
         */
        public String theme_url()
        {
            return site_url(iBlogsConst.TEMPLATES + iBlogsConst.THEME);
        }

        /**
         * 返回主题下的文件路径
         *
         * @param sub
         * @return
         */
        public String theme_url(String sub)
        {
            return site_url(iBlogsConst.TEMPLATES + iBlogsConst.THEME + sub);
        }


        /**
         * 返回gravatar头像地址
         *
         * @param email
         * @return
         */
        public String gravatar(String email)
        {
            String avatarUrl = "https://cn.gravatar.com/avatar";
            if (stringKit.isBlank(email))
            {
                return avatarUrl;
            }
            String hash = CreateMD5(email.Trim().ToLowerInvariant());
            return avatarUrl + "/" + hash;
        }
        private string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }

        /**
         * 格式化unix时间戳为日期
         *
         * @param unixTime
         * @return
         */
        public String fmtdate(long unixTime)
        {
            return Convert.ToDateTime(unixTime).ToString("yyyy-MM-dd");
        }

        /**
         * 格式化日期
         *
         * @param date
         * @param fmt
         * @return
         */
        public String fmtdate(DateTime date, String fmt)
        {
            return date.ToString(fmt);
        }

        /**
         * 格式化unix时间戳为日期
         *
         * @param unixTime
         * @param patten
         * @return
         */
        public String fmtdate(int unixTime, String patten)
        {
            if (null != unixTime && stringKit.isNotBlank(patten))
            {
                return Convert.ToDateTime(unixTime).ToString(patten);
            }
            return "";
        }

        /**
         * 获取随机数
         *
         * @param max
         * @param str
         * @return
         */
        public String random(int max, String str)
        {
            return new Random().Next(1, max) + str;
        }

        /**
         * An :grinning:awesome :smiley:string &#128516;with a few :wink:emojis!
         * <p>
         * 这种格式的字符转换为emoji表情
         *
         * @param value
         * @return
         */
        public String emoji(String value)
        {
            throw new NotImplementedException();
        }

        // private final Pattern SRC_PATTERN = Pattern.compile("src\\s*=\\s*\'?\"?(.*?)(\'|\"|>|\\s+)");
        /**
         * 获取文章第一张图片
         *
         * @return
         */
        public String show_thumb(String content)
        {
            //content = TaleUtils.mdToHtml(content);
            //if (content.contains("<img"))
            //{
            //    String img = "";
            //    String regEx_img = "<img.*src\\s*=\\s*(.*?)[^>]*?>";
            //    Pattern p_image = Pattern.compile(regEx_img, Pattern.CASE_INSENSITIVE);
            //    Matcher m_image = p_image.matcher(content);
            //    if (m_image.find())
            //    {
            //        img = img + "," + m_image.group();
            //        // //匹配src
            //        Matcher m = SRC_PATTERN.matcher(img);
            //        if (m.find())
            //        {
            //            return m.group(1);
            //        }
            //    }
            //}
            return "";
        }
    }

}

