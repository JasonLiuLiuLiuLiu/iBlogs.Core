using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using iBlogs.Site.Core.Common.Extensions;
using Microsoft.Extensions.Logging;

namespace iBlogs.Site.Core.Git
{
    public class GitBlogService : IGitBlogService
    {
        private const string headerFormat = "\r\n<!--This tag was auto generate by iBlogs--\r\nTitle       : {0}\r\nTags        : {1}\r\nCategories  : {2}\r\nBlogId      : {3}\r\nLastUpdate  : {4}\r\nMessage     : {5}\r\n-- https://github.com/liuzhenyulive/iBlogs -->\r\n";
        private const string headerPre = "<!--";
        private const string headerEnd = "-->";
        private readonly ILogger<GitBlogService> _logger;

        public GitBlogService(ILogger<GitBlogService> logger)
        {
            _logger = logger;
        }


        public async Task<bool> Handle(string filePath)
        {
            string fileText = File.ReadAllText(filePath, Encoding.UTF8);
            if (TryGetHeaderContext(fileText, out MarkDownHeaderContext headerContext))
            {
                headerContext.Title = headerContext.Title ?? Path.GetFileNameWithoutExtension(filePath);
            }
            else
            {
                headerContext.Title = headerContext.Title ?? Path.GetFileNameWithoutExtension(filePath);
                fileText = string.Format(headerFormat, headerContext.Title, headerContext.Tags,
                               headerContext.Categories, headerContext.BlogId, headerContext.LastUpdate,
                               headerContext.Message) + fileText;
            }

            var encodedText = Encoding.UTF8.GetBytes(fileText);

            using (var sourceStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None, bufferSize: 4096, useAsync: true))
            {
                await sourceStream.WriteAsync(encodedText, 0, encodedText.Length);
            };
            return true;
        }

        private bool TryGetHeaderContext(string content, out MarkDownHeaderContext context)
        {
            context = new MarkDownHeaderContext();
            var headerPreIndex = content.IndexOf(headerPre, StringComparison.CurrentCultureIgnoreCase);
            var headerEndIndex = content.IndexOf(headerEnd, StringComparison.CurrentCultureIgnoreCase);
            if (headerPreIndex >= headerEndIndex)
                return false;
            try
            {
                var headerContent = content.Substring(headerPreIndex, headerEndIndex - headerPreIndex);
                var headerMap = headerContent.SplitToLines().Select(u => u.Split(':')).Where(u => u.Length == 2).ToDictionary(u => u[0].Trim(), u => u[1].Trim());
                foreach (var property in context.GetType().GetProperties())
                {
                    if (headerMap.ContainsKey(property.Name))
                        property.SetValue(context, Convert.ChangeType(headerMap, property.PropertyType));
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                context.Message = "输入标签格式有误";
            }

            return true;
        }
    }
}
