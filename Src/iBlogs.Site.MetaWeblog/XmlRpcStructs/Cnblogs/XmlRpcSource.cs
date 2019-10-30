using CookComputing.XmlRpc;

namespace iBlogs.Site.MetaWeblog.XmlRpcStructs.Cnblogs
{
    [XmlRpcMissingMapping(MappingAction.Ignore)]
    public struct XmlRpcSource
    {
        public string name;
        public string url;
    }
}