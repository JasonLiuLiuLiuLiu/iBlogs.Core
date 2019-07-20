using CookComputing.XmlRpc;

namespace iBlogs.Site.MetaWeblog.CnBlogs.XmlRpcStructs
{
    [XmlRpcMissingMapping(MappingAction.Ignore)]
    public struct WpCategory
    {
        public string name;
        public string slug;
        public int parent_id;
        public string description;
    }
}