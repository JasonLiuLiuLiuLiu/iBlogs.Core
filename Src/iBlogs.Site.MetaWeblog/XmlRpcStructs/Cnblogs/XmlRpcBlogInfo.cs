using CookComputing.XmlRpc;

namespace iBlogs.Site.MetaWeblog.XmlRpcStructs.Cnblogs
{
    [XmlRpcMissingMapping(MappingAction.Ignore)]
    public struct XmlRpcBlogInfo
    {
        public string blogid;
        public string url;
        public string blogName;
    }
}