using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using iBlogs.Site.Application.Extensions;
using Markdig;
using Microsoft.Extensions.FileSystemGlobbing;

namespace iBlogs.Site.Application.Utils
{
    public class IBlogsUtils
    {
        /**
        * 提取html中的文字
        */
        public static String htmlToText(String html)
        {
            var regexCss = new Regex("(?s) <[^>] *> (\\s *<[^>] *>) * ", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            if (stringKit.isNotBlank(html))
            {
                return regexCss.Replace(html, string.Empty);
            }
            return "";
        }

        public static string getFileName(string path)
        {
            return Path.GetFileName(path);
        }

        /**
 * 获取文章第一张图片
 *
 * @return
 */
        public static String show_thumb(String content)
        {
            content = Markdown.ToHtml(content);
            if (content.Contains("<img"))
            {
                String img = "";
                String regEx_img = "<img.*src\\s*=\\s*(.*?)[^>]*?>";
                var regex = new Regex(regEx_img, RegexOptions.Singleline | RegexOptions.IgnoreCase);
                var m_image = regex.Match(content);
                if (m_image.Success)
                {
                    return img + "," + m_image.Value;

                }
            }
            return "";
        }
    }
}
