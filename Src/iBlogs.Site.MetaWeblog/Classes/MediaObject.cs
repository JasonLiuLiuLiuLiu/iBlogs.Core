﻿namespace iBlogs.Site.MetaWeblog.Classes
{
	/// <summary>
	/// Represents a Media Object - this is usually an image, video, document etc..
	/// </summary>
	public class MediaObject
	{
		/// <summary>
		/// The name of the Media Object.
		/// </summary>
        public string Name { get; set; }

		/// <summary>
		/// The type of the Media Object.
		/// </summary>
        public string Type { get; set; }

		/// <summary>
		/// The byte array of the Media Object itself.
		/// 
		/// </summary>
        public byte[] Bits { get; set; }

        //todo: add method to get byte[] from file etc... make it easier for user
	}
}
