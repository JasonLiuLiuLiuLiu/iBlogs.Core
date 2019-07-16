using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iBlogs.Site.Core.Common.Extensions;

namespace iBlogs.Site.Core.Git
{
    public class GitBlogService : IGitBlogService
    {
        private static string template = "<===============================================================\r\nTitle:\r\nTag:\r\nCategory:\r\n===============================================================\r\nLastUpdateTime:\r\nBlogId:\r\n===============================================================>\r\n";

        public async Task<bool> Handle(string filePath)
        {
            string fileText = File.ReadAllText(filePath, Encoding.UTF8);
            fileText += DateTime.Now.ToString(CultureInfo.InvariantCulture);
            var lines = fileText.SplitToLines();
            var firstLine = Array.IndexOf(lines, lines.FirstOrDefault(u => u.StartsWith("<") && u.EndsWith("=")));
            var lastLine = Array.IndexOf(lines, lines.LastOrDefault(u => u.StartsWith("=") && u.EndsWith(">")));
            if (firstLine >= lastLine)
                fileText = template + fileText;

            byte[] encodedText = Encoding.UTF8.GetBytes(fileText);

            using (FileStream sourceStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None, bufferSize: 4096, useAsync: true))
            {
                await sourceStream.WriteAsync(encodedText, 0, encodedText.Length);
            };
            return true;
        }
    }
}
