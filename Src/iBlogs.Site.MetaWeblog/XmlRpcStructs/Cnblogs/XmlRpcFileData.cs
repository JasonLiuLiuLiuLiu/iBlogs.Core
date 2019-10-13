using CookComputing.XmlRpc;

namespace iBlogs.Site.MetaWeblog.XmlRpcStructs.Cnblogs
{
    [XmlRpcMissingMapping(MappingAction.Ignore)]
    public struct XmlRpcFileData
    {
        public byte[] bits;
        public string name;
        public string type;
    }
}
