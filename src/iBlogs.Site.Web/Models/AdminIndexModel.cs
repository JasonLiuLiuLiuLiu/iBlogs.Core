using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iBlogs.Site.Core.Content;

namespace iBlogs.Site.Web.Models
{
    public class AdminIndexModel
    {
        public List<Contents> Articles;
        public int ArticlesCount;
        public int CommentsCount;
        public int AttachCount;
    }
}
