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
        public object postid;
        public Source source;
        public string userid;

        public object mt_allow_comments;
        public object mt_allow_pings;
        public object mt_convert_breaks;
        public string mt_text_more;

        public string mt_excerpt;
        public string mt_keywords;

        public string wp_slug;
    }

}