using CookComputing.XmlRpc;

namespace iBlogs.Site.MetaWeblog.XmlRpcStructs
{
	/// <summary>
	/// Represents media object info - The URL to the media object.
	/// </summary>
	[XmlRpcMissingMapping(MappingAction.Ignore)]
    public struct XmlRpcMediaObjectInfo
	{
		/// <summary>
		/// The URL to the media object.
		/// </summary>
		public string url;
	}
}