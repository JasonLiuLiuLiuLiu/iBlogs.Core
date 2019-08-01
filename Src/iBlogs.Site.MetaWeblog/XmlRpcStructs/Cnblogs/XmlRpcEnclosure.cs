using System;
using System.Collections.Generic;
using System.Text;
using CookComputing.XmlRpc;

namespace iBlogs.Site.MetaWeblog.CnBlogs.XmlRpcStructs
{
    [XmlRpcMissingMapping(MappingAction.Ignore)]
    public struct XmlRpcEnclosure
    {
        public int length;
        public string type;
        public string url;
    }
}
