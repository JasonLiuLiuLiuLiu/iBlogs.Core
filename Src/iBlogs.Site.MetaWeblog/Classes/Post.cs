using System;

namespace iBlogs.Site.MetaWeblog.Classes
{
   
    public class Post
    {
        public DateTime DateCreated{get;set;}
        public string Description{get;set;}
        public string Title{get;set;}

        public string[] Categories{get;set;}
        public Enclosure Enclosure{get;set;}
        public string Link{get;set;}
        public string Permalink{get;set;}
        public object PostId{get;set;}
        public Source Source{get;set;}
        public string UserId{get;set;}

        public object MtAllowComments{get;set;}
        public object MtAllowPings{get;set;}
        public object MtConvertBreaks{get;set;}
        public string MtTextMore{get;set;}

        public string MtExcerpt{get;set;}
        public string MtKeywords{get;set;}

        public string WpSlug{get;set;}
    }
}