using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using iBlogs.Site.Core.Content;

namespace iBlogs.Site.Core.Common.AutoMapper
{
   public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ContentInput, Contents>();
            CreateMap<Contents, ContentInput>();
        }
    }
}
