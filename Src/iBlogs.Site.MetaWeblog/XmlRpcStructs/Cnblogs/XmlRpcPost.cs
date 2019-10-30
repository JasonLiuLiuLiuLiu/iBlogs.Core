using System;
using CookComputing.XmlRpc;

namespace iBlogs.Site.MetaWeblog.XmlRpcStructs.Cnblogs
{
    [XmlRpcMissingMapping(MappingAction.Ignore)]
    public struct XmlRpcPost
    {
        public DateTime dateCreated;
        public string description;
        public string title;

        public string[] categories;
        public XmlRpcEnclosure XML_RPC_ENCLOSURE;
        public string link;
        public string permalink;
        public object postid;
        public XmlRpcSource XML_RPC_SOURCE;
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