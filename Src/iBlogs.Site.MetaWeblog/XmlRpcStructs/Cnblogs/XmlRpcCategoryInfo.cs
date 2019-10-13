using CookComputing.XmlRpc;

namespace iBlogs.Site.MetaWeblog.XmlRpcStructs.Cnblogs
{
    [XmlRpcMissingMapping(MappingAction.Ignore)]
    public struct XmlRpcCategoryInfo
    {
        public string description;
        public string htmlUrl;
        public string rssUrl;
        public string title;
        public string categoryid;
    }
}