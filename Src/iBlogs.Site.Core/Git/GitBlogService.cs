using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace iBlogs.Site.Core.Git
{
    public class GitBlogService : IGitBlogService
    {
        public async Task<bool> Handle(string filePath)
        {
            string fileText = null;
            using (var reader = File.OpenText(filePath))
            {
                fileText = await reader.ReadToEndAsync();
            }
            fileText += DateTime.Now.ToString(CultureInfo.InvariantCulture);
            byte[] encodedText = Encoding.UTF8.GetBytes(fileText);

            using (FileStream sourceStream = new FileStream(filePath,FileMode.Create, FileAccess.Write, FileShare.None,bufferSize: 4096, useAsync: true))
            {
                await sourceStream.WriteAsync(encodedText, 0, encodedText.Length);
            };
            return true;
        }
    }
}
