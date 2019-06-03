using AutoMapper;
using iBlogs.Site.Core.Comment;
using iBlogs.Site.Core.Comment.DTO;
using iBlogs.Site.Core.Content;
using iBlogs.Site.Core.Content.DTO;

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
