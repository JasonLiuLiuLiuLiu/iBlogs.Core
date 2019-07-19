using AutoMapper;
using iBlogs.Site.Core.Blog.Comment;
using iBlogs.Site.Core.Blog.Comment.DTO;
using iBlogs.Site.Core.Blog.Content;
using iBlogs.Site.Core.Blog.Content.DTO;

namespace iBlogs.Site.Core.Common.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ContentInput, Contents>();
            CreateMap<Contents, ContentInput>();
            CreateMap<Contents, ContentResponse>();
            CreateMap<ContentResponse, Contents>();
            CreateMap<Comments, CommentResponse>();
            CreateMap<CommentResponse, Comments>();
        }
    }
}
