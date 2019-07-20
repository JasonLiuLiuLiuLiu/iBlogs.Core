using CookComputing.XmlRpc;

namespace iBlogs.Site.MetaWeblog.CnBlogs.XmlRpcStructs
{
    [XmlRpcMissingMapping(MappingAction.Ignore)]
    public struct CategoryInfo
    {
        public string description;
        public string htmlUrl;
        public string rssUrl;
        public string title;
        public string categoryid;
    }
}