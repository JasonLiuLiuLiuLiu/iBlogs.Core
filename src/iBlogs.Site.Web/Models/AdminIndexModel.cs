using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iBlogs.Site.Core.Content;
using iBlogs.Site.Core.Content.DTO;

namespace iBlogs.Site.Web.Models
{
    public class AdminIndexModel
    {
        public List<ContentResponse> Articles;
        public int ArticlesCount;
        public int CommentsCount;
        public int AttachCount;
    }
}
