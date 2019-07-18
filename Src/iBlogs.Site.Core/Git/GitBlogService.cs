using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using iBlogs.Site.Core.Blog.Content;
using iBlogs.Site.Core.Blog.Content.DTO;
using iBlogs.Site.Core.Blog.Content.Service;
using iBlogs.Site.Core.Common.Extensions;
using Microsoft.Extensions.Logging;

namespace iBlogs.Site.Core.Git
{
    public class GitBlogService : IGitBlogService
    {
        private const string HeaderFormat = "<!--This tag was auto generate by iBlogs--\r\nTitle       : {0}\r\nTags        : {1}\r\nCategories  : {2}\r\nBlogId      : {3}\r\nStatus      : {4}\r\nLastUpdate  : {5}\r\nMessage     : {6}\r\n-- https://github.com/liuzhenyulive/iBlogs -->";
        private const string HeaderPre = "<!--";
        private const string HeaderEnd = "-->";
        private const string DefaultMessage = "请按要求修改以上标签";
        private readonly ILogger<GitBlogService> _logger;
        private readonly IContentsService _contentsService;

        public GitBlogService(ILogger<GitBlogService> logger, IContentsService contentsService)
        {
            _logger = logger;
            _contentsService = contentsService;
        }


        public async Task<bool> Handle(string filePath)
        {
            string fileText = File.ReadAllText(filePath, Encoding.UTF8);
            var headerContext = new MarkDownHeaderContext
            {
                Title = Path.GetFileNameWithoutExtension(filePath),
                Categories = "默认分类"
            };
            if (TryGetHeaderContext(fileText, headerContext) && headerContext.BlogId != 0)
            {
                UpdateBlog(fileText, headerContext);
            }
            else
            {
                InsertBlog(fileText, headerContext);
            }

            var encodedText = Encoding.UTF8.GetBytes(UpdateHeader(fileText, headerContext));

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

            var headerPreIndex = content.IndexOf(HeaderPre, StringComparison.CurrentCultureIgnoreCase);
            var headerEndIndex = content.IndexOf(HeaderEnd, StringComparison.CurrentCultureIgnoreCase);
            if (headerPreIndex >= headerEndIndex)
                return false;
            try
            {
                var headerContent = content.Substring(headerPreIndex, headerEndIndex - headerPreIndex);
                var headerMap = headerContent.SplitToLines().Select(u => u.Split(':')).Where(u => u.Length == 2).ToDictionary(u => u[0].Trim(), u => u[1].Trim());
                foreach (var property in context.GetType().GetProperties())
                {
                    if (headerMap.ContainsKey(property.Name))
                        property.SetValue(context, Convert.ChangeType(headerMap[property.Name], property.PropertyType));
                }

                context.Message = DefaultMessage;
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                context.Message = "输入标签格式有误";
            }

            return true;
        }

        private bool InsertBlog(string contentText, MarkDownHeaderContext context)
        {
            contentText = GetContentWithoutHeader(contentText);
            var content = new ContentInput
            {
                Title = context.Title,
                Tags = context.Tags,
                Categories = context.Categories,
                Content = contentText,
                Status = Enum.IsDefined(typeof(ContentStatus), context.Status)
                    ? (ContentStatus) context.Status
                    : ContentStatus.Draft
            };
            context.BlogId = _contentsService.UpdateArticle(content);
            return true;
        }

        private bool UpdateBlog(string contentText, MarkDownHeaderContext context)
        {
            contentText = GetContentWithoutHeader(contentText);
            var content = new ContentInput
            {
                Id = context.BlogId,
                Title = context.Title,
                Tags = context.Tags,
                Categories = context.Categories,
                Content = contentText,
                Status = Enum.IsDefined(typeof(ContentStatus), context.Status)
                    ? (ContentStatus)context.Status
                    : ContentStatus.Draft
            };
            context.BlogId = _contentsService.UpdateArticle(content);
            return true;
        }

        private string UpdateHeader(string content, MarkDownHeaderContext context)
        {
            var contentBuilder = new StringBuilder();
            contentBuilder.AppendLine();
            contentBuilder.AppendFormat(HeaderFormat, context.Title, context.Tags, context.Categories, context.BlogId,
                context.Status, context.LastUpdate, context.Message);
            contentBuilder.AppendLine();
            contentBuilder.AppendLine();
            contentBuilder.AppendLine(GetContentWithoutHeader(content));
            return contentBuilder.ToString();
        }

        private string GetContentWithoutHeader(string content)
        {
            var headerEndIndex = content.IndexOf(HeaderEnd, StringComparison.CurrentCultureIgnoreCase);
            headerEndIndex = headerEndIndex == -1 ? 0 : headerEndIndex + HeaderPre.Length - 1;
            return content.Substring(headerEndIndex, content.Length - headerEndIndex).TrimStart();
        }
    }
}
