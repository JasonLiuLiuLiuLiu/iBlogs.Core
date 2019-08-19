using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Markdig;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace iBlogs.Site.Core.Common
{
    public class BlogsUtils
    {
        /**
        * 提取html中的文字
        */
        public static string HtmlToText(string html)
        {
            string htmlTagPattern = "<.*?>";
            var regexCss = new Regex("(\\<script(.+?)\\</script\\>)|(\\<style(.+?)\\</style\\>)", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            html = regexCss.Replace(html, string.Empty);
            html = Regex.Replace(html, htmlTagPattern, string.Empty);
            html = Regex.Replace(html, @"^\s+$[\r\n]*", "", RegexOptions.Multiline);
            html = html.Replace("&nbsp;", string.Empty);

            return html;
        }

        public static string GetFileName(string path)
        {
            return Path.GetFileName(path);
        }

        /**
        * 获取文章第一张图片
        *
        * @return
        */
        public static string ShowThumb(string content)
        {
            content = Markdown.ToHtml(content);
            if (content.Contains("<img"))
            {
                return Regex.Match(content, "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1].Value;
            }
            return "";
        }

        public static string BuildUrl(String url)
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
        public static string Md5(string data, string salt)
        {
            return Md5(Encoding.ASCII.GetBytes((data + salt)));
        }

        /**
         * MD5加密
         *
         * @param data 明文字节数组
         * @return 16进制密文
         */
        public static string Md5(byte[] input)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                var data = sha256Hash.ComputeHash(input);
                // Create a new StringBuilder to collect the bytes
                // and create a string.
                var sBuilder = new StringBuilder();

                // Loop through each byte of the hashed data 
                // and format each one as a hexadecimal string.
                foreach (var t in data)
                {
                    sBuilder.Append(t.ToString("x2"));
                }
                // Return the hexadecimal string.
                return sBuilder.ToString();
            }
        }

        public static String GetFileKey(String name, string rootPath)
        {
            String prefix = "/upload/" + DateTime.Now.ToString("yyyy/MM");
            String dir = rootPath + prefix;
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            return prefix + "/" + Guid.NewGuid() + "." + Path.GetExtension(name);
        }

        /**
        * 判断文件是否是图片类型
        */
        public static bool IsImage(string imageFile)
        {
            if (!File.Exists(imageFile))
            {
                return false;
            }
            return new[] { ".JPG", ".JPE", ".BMP", ".GIF", ".PNG" }.Contains(Path.GetExtension(imageFile)?.ToUpper());
        }

        /**
        * 根据尺寸图片居中裁剪
        *
        * @param src
        * @param dist
        * @param w
        * @param h
        * @throws IOException
        */
        public static void CutCenterImage(string src, string dist, int w, int h)
        {
            // Image.Load(string path) is a shortcut for our default type. 
            // Other pixel formats use Image.Load<TPixel>(string path))
            using (Image<Rgba32> image = Image.Load(src))
            {
                image.Mutate(x => x
                    .Resize(w, h));
                image.Save(dist); // Automatic encoder selected based on extension.
            }
        }

    }
}
