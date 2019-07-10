using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iBlogs.Site.Core.Blog.Comment.DTO;
using iBlogs.Site.Core.Blog.Content.DTO;

namespace iBlogs.Site.Web.Models
{
    public class AdminIndexModel
    {
        public List<ContentResponse> Articles;
        public List<CommentResponse> Comments;
        public int ArticlesCount;
        public int CommentsCount;
        public int AttachCount;
    }
}
