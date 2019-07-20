using System;
using CookComputing.XmlRpc;

namespace iBlogs.Site.MetaWeblog.CnBlogs.XmlRpcStructs
{
    [XmlRpcMissingMapping(MappingAction.Ignore)]
    public struct Post
    {
        public DateTime dateCreated;
        public string description;
        public string title;
        public string[] categories;
        public Enclosure enclosure;
        public string link;
        public string permalink;
        public int postid;
        public Source source;
        public string userid;
        public int mt_allow_comments;
        public int mt_allow_pings;
        public int mt_convert_breaks;
        public string mt_text_more;
        public string mt_excerpt;
        public string mt_keywords;
        public string wp_slug;
    }
}