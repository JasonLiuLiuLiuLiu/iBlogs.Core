﻿using CookComputing.XmlRpc;

namespace iBlogs.Site.MetaWeblog.XmlRpcStructs
{
    /// <summary>
    /// A comment on a blog item
    /// </summary>
    [XmlRpcMissingMapping(MappingAction.Ignore)]
    public struct XmlRpcNewComment
    {
        public int post_id;

        public int comment_parent;
        public string content;
        public string author;
        public string author_url;
        public string author_email;
    }
}