using CookComputing.XmlRpc;

namespace iBlogs.Site.MetaWeblog.CnBlogs.XmlRpcStructs
{
    [XmlRpcMissingMapping(MappingAction.Ignore)]
    public struct BlogInfo
    {
        public string blogid;
        public string url;
        public string blogName;
    }
}