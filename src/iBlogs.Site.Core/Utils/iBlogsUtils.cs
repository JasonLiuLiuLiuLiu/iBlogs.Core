using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using iBlogs.Site.Core.Extensions;
using Markdig;
using Microsoft.Extensions.FileSystemGlobbing;

namespace iBlogs.Site.Core.Utils
{
    public class IBlogsUtils
    {
        /**
        * 提取html中的文字
        */
        public static string htmlToText(string html)
        {
            string htmlTagPattern = "<.*?>";
            var regexCss = new Regex("(\\<script(.+?)\\</script\\>)|(\\<style(.+?)\\</style\\>)", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            html = regexCss.Replace(html, string.Empty);
            html = Regex.Replace(html, htmlTagPattern, string.Empty);
            html = Regex.Replace(html, @"^\s+$[\r\n]*", "", RegexOptions.Multiline);
            html = html.Replace("&nbsp;", string.Empty);

            return html;
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
        public static string show_thumb(string content)
        {
            content = Markdown.ToHtml(content);
            if (content.Contains("<img"))
            {
                string img = "";
                string regEx_img = "<img.*src\\s*=\\s*(.*?)[^>]*?>";
                var regex = new Regex(regEx_img, RegexOptions.Singleline | RegexOptions.IgnoreCase);
                var m_image = regex.Match(content);
                if (m_image.Success)
                {
                    return img + "," + m_image.Value;

                }
            }
            return "";
        }

        public static string buildURL(String url)
        {
            if (url.EndsWith("/"))
            {
                url = url.Substring(0, url.Length - 1);
            }
            if (!url.StartsWith("http"))
            {
                url = "http://" + url;
            }
            return url;
        }

        /**
 * MD5加密
 *
 * @param data 明文字符串
 * @param salt 盐
 * @return 16进制加盐密文
 */
        public static string md5(string data, string salt)
        {
            return md5(Encoding.ASCII.GetBytes((data + salt)));
        }

        /**
         * MD5加密
         *
         * @param data 明文字节数组
         * @return 16进制密文
         */
        public static string md5(byte[] input)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                var data = sha256Hash.ComputeHash(input);
                // Create a new Stringbuilder to collect the bytes
                // and create a string.
                var sBuilder = new StringBuilder();

                // Loop through each byte of the hashed data 
                // and format each one as a hexadecimal string.
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }
                // Return the hexadecimal string.
                return sBuilder.ToString();
            }
        }
    }
}
