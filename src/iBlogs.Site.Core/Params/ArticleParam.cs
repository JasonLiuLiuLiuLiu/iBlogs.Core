using System;
using System.Collections.Generic;
using System.Text;

namespace iBlogs.Site.Core.Params
{
    public class ArticleParam : PageParam
    {

        public string Title{get;set;}
        public string Categories{get;set;}
        public string Status{get;set;}
        public string Type{get;set;}
        public string OrderBy{get;set;}

    }
}
