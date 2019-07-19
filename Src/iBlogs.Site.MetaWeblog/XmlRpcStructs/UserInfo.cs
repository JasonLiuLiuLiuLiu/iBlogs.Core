using CookComputing.XmlRpc;

namespace iBlogs.Site.MetaWeblog.XmlRpcStructs
{
	/// <summary> 
	/// This struct represents information about a user. 
	/// </summary> 
	[XmlRpcMissingMapping(MappingAction.Ignore)]
    public struct XmlRpcUserInfo
	{
		public string url;
		public int blogId;
		public string blogName;
		public string firstname;
		public string lastname;
		public string email;
		public string nickname;
	}
}