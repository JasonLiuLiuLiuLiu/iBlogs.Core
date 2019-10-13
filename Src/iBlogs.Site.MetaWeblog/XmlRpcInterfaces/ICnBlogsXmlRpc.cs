using CookComputing.XmlRpc;
using iBlogs.Site.MetaWeblog.XmlRpcStructs.Cnblogs;

namespace iBlogs.Site.MetaWeblog.XmlRpcInterfaces
{
    /// <summary>
    /// https://rpc.cnblogs.com/metaweblog/CoderAyu
    /// </summary>
    public interface ICnBlogsXmlRpc: IMetaWeblogXmlRpc
    {
        /// <summary>
        /// Deletes a xmlRpcPost.
        /// </summary>
        /// <param name="appKey"></param>
        /// <param name="postid"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="publish">Where applicable, this specifies whether the blog should be republished after the xmlRpcPost has been deleted.</param>
        /// <returns>Always returns true.</returns>
       [XmlRpcMethod("blogger.deletePost")]
        bool DeletePost(string appKey, string postid, string username, string password, bool publish);
        /// <summary>
        /// Returns information on all the blogs a given user is a member.
        /// </summary>
        /// <param name="appKey"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [XmlRpcMethod("blogger.getUsersBlogs")]
        XmlRpcBlogInfo[] GetUsersBlogs(string appKey, string username, string password);
        /// <summary>
        /// Updates and existing xmlRpcPost to a designated blog using the metaWeblog API. Returns true if completed.
        /// </summary>
        /// <param name="postid"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="xmlRpcPost"></param>
        /// <param name="publish"></param>
        /// <returns></returns>
        [XmlRpcMethod("metaWeblog.editPost")]
        object EditPost(string postid, string username, string password, XmlRpcPost xmlRpcPost, bool publish);
        /// <summary>
        /// Retrieves a list of valid categories for a xmlRpcPost using the metaWeblog API. Returns the metaWeblog categories struct collection.
        /// </summary>
        /// <param name="blogid"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [XmlRpcMethod("metaWeblog.getCategories")]
        XmlRpcCategoryInfo[] GetCategories(string blogid,string username,string password);
        /// <summary>
        /// Retrieves an existing xmlRpcPost using the metaWeblog API. Returns the metaWeblog struct.
        /// </summary>
        /// <param name="postid"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [XmlRpcMethod("metaWeblog.getPost")]
        XmlRpcPost GetPost(string postid, string username, string password);
        /// <summary>
        /// Retrieves a list of the most recent existing xmlRpcPost using the metaWeblog API. Returns the metaWeblog struct collection.
        /// </summary>
        /// <param name="blogid"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="numberOfPosts"></param>
        /// <returns></returns>
        [XmlRpcMethod("metaWeblog.getRecentPosts")]
        XmlRpcPost[] GetRecentPosts(string blogid, string username, string password, int numberOfPosts);
        /// <summary>
        /// Makes a new xmlRpcFile to a designated blog using the metaWeblog API. Returns url as a string of a struct.
        /// </summary>
        /// <param name="blogid"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="xmlRpcFile"></param>
        /// <returns></returns>
        [XmlRpcMethod("metaWeblog.newMediaObject")]
        XmlRpcUrlData NewMediaObject(string blogid, string username, string password, XmlRpcFileData xmlRpcFile);
        /// <summary>
        /// Makes a new xmlRpcPost to a designated blog using the metaWeblog API. Returns postid as a string.
        /// </summary>
        /// <param name="blogid"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="xmlRpcPost"></param>
        /// <param name="publish"></param>
        /// <returns></returns>
        [XmlRpcMethod("metaWeblog.newPost")]
        string NewPost(string blogid, string username, string password, XmlRpcPost xmlRpcPost,bool publish);
        /// <summary>
        /// Create a new category
        /// </summary>
        /// <param name="blog_id"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        [XmlRpcMethod("wp.newCategory")]
        int NewCategory(string blog_id, string username, string password, XmlRpcWpCategory category);
    }
}