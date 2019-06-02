using AutoMapper;
using iBlogs.Site.Core.Content;
using iBlogs.Site.Core.Content.DTO;

namespace iBlogs.Site.Core.Common.AutoMapper
{
   public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ContentInput, Contents>();
            CreateMap<Contents, ContentInput>();
            CreateMap<Contents,ContentResponse>();
            CreateMap<ContentResponse, Contents>();
        }
    }
}
