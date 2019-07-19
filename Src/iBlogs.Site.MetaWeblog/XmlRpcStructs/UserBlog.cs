using CookComputing.XmlRpc;

namespace iBlogs.Site.MetaWeblog.XmlRpcStructs
{
	/// <summary> 
	/// This struct represents information about a user's blog. 
	/// </summary> 
	[XmlRpcMissingMapping(MappingAction.Ignore)]
    public struct XmlRpcUserBlog
	{
		public bool isAdmin;
		public string url;
		public int blogId;
		public string blogName;
		public string xmlrpc;
	}
}