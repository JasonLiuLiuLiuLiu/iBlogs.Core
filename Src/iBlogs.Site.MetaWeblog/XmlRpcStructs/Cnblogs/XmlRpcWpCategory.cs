using CookComputing.XmlRpc;

namespace iBlogs.Site.MetaWeblog.XmlRpcStructs.Cnblogs
{
    [XmlRpcMissingMapping(MappingAction.Ignore)]
    public struct XmlRpcWpCategory
    {
        public string name;
        public string slug;
        public int parent_id;
        public string description;
    }
}