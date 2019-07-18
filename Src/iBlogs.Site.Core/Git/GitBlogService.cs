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
        private const string headerFormat = "<!--This tag was auto generate by iBlogs--\r\nTitle       : {0}\r\nTags        : {1}\r\nCategories  : {2}\r\nBlogId      : {3}\r\nStatus      : {4}\r\nLastUpdate  : {5}\r\nMessage     : {6}\r\n-- https://github.com/liuzhenyulive/iBlogs -->";
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
            var headerContext = new MarkDownHeaderContext
            {
                Title = Path.GetFileNameWithoutExtension(filePath)
            };
            if (TryGetHeaderContext(fileText, headerContext))
            {
                UpdateBlog(fileText, headerContext);
               
            }
            else
            {
                InsertBlog(fileText, headerContext);
            }

            var encodedText = Encoding.UTF8.GetBytes(UpdateHeader(fileText,headerContext));

            using (var sourceStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None, bufferSize: 4096, useAsync: true))
            {
                await sourceStream.WriteAsync(encodedText, 0, encodedText.Length);
            }
            return true;
        }

        private bool TryGetHeaderContext(string content, MarkDownHeaderContext context)
        {
            if (context == null)
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

        private bool InsertBlog(string content, MarkDownHeaderContext context)
        {
            return true;
        }

        private bool UpdateBlog(string content, MarkDownHeaderContext context)
        {
            return true;
        }

        private string UpdateHeader(string content, MarkDownHeaderContext context)
        {
            var headerEndIndex = content.IndexOf(headerEnd, StringComparison.CurrentCultureIgnoreCase);
            headerEndIndex = headerEndIndex == -1 ? 0 : headerEndIndex+headerPre.Length;
            content = content.Substring(headerEndIndex, content.Length - headerEndIndex);
            var contentBuilder=new StringBuilder();
            contentBuilder.AppendLine();
            contentBuilder.AppendFormat(headerFormat, context.Title, context.Tags, context.Categories, context.BlogId,
                context.Status, context.LastUpdate, context.Message);
            contentBuilder.AppendLine();
            contentBuilder.AppendLine();
            contentBuilder.AppendLine(content);
            return contentBuilder.ToString();
        }
    }
}
