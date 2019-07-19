﻿using System;
using CookComputing.XmlRpc;

namespace iBlogs.Site.MetaWeblog.XmlRpcStructs
{
	/// <summary>
	/// Page.
	/// </summary>
	[XmlRpcMissingMapping(MappingAction.Ignore)]
    public struct XmlRpcPageMin
	{
		public DateTime dateCreated;
		public string page_id;
		public string page_title;
		public object page_parent_id;
	}
}