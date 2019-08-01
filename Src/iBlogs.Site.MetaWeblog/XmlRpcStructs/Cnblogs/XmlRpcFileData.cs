using System;
using System.Collections.Generic;
using System.Text;
using CookComputing.XmlRpc;

namespace iBlogs.Site.MetaWeblog.CnBlogs.XmlRpcStructs
{
    [XmlRpcMissingMapping(MappingAction.Ignore)]
    public struct XmlRpcFileData
    {
        public byte[] bits;
        public string name;
        public string type;
    }
}
