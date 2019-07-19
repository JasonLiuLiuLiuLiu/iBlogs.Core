using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace iBlogs.Site.Core.Git
{
    public interface IGitBlogService
    {
        Task<bool> Handle(string filePath);
    }
}
