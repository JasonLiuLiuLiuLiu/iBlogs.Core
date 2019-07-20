using CookComputing.XmlRpc;
using iBlogs.Site.MetaWeblog.CnBlogs.XmlRpcStructs;
using iBlogs.Site.MetaWeblog.XmlRpcInterfaces;

namespace iBlogs.Site.MetaWeblog.CnBlogs
{
    /// <summary>
    /// https://rpc.cnblogs.com/metaweblog/CoderAyu
    /// </summary>
    public interface ICnBlogsXmlRpc: IMetaWeblogXmlRpc
    {
        /// <summary>
        /// Deletes a post.
        /// </summary>
        /// <param name="appKey"></param>
        /// <param name="postid"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="publish">Where applicable, this specifies whether the blog should be republished after the post has been deleted.</param>
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
        BlogInfo[] GetUsersBlogs(string appKey, string username, string password);
        /// <summary>
        /// Updates and existing post to a designated blog using the metaWeblog API. Returns true if completed.
        /// </summary>
        /// <param name="postid"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="post"></param>
        /// <param name="publish"></param>
        /// <returns></returns>
        [XmlRpcMethod("metaWeblog.editPost")]
        int EditPost(string postid, string username, string password, Post post, bool publish);
        /// <summary>
        /// Retrieves a list of valid categories for a post using the metaWeblog API. Returns the metaWeblog categories struct collection.
        /// </summary>
        /// <param name="blogid"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [XmlRpcMethod("metaWeblog.getCategories")]
        CategoryInfo[] GetCategories(string blogid,string username,string password);
        /// <summary>
        /// Retrieves an existing post using the metaWeblog API. Returns the metaWeblog struct.
        /// </summary>
        /// <param name="postid"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [XmlRpcMethod("metaWeblog.getPost")]
        Post GetPost(string postid, string username, string password);
        /// <summary>
        /// Retrieves a list of the most recent existing post using the metaWeblog API. Returns the metaWeblog struct collection.
        /// </summary>
        /// <param name="blogid"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="numberOfPosts"></param>
        /// <returns></returns>
        [XmlRpcMethod("metaWeblog.getRecentPosts")]
        Post[] GetRecentPosts(string blogid, string username, string password, int numberOfPosts);
        /// <summary>
        /// Makes a new file to a designated blog using the metaWeblog API. Returns url as a string of a struct.
        /// </summary>
        /// <param name="blogid"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        [XmlRpcMethod("metaWeblog.newMediaObject")]
        UrlData NewMediaObject(string blogid, string username, string password, FileData file);
        /// <summary>
        /// Makes a new post to a designated blog using the metaWeblog API. Returns postid as a string.
        /// </summary>
        /// <param name="blogid"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="post"></param>
        /// <param name="publish"></param>
        /// <returns></returns>
        [XmlRpcMethod("metaWeblog.newPost")]
        string NewPost(string blogid, string username, string password, Post post,bool publish);
        /// <summary>
        /// Create a new category
        /// </summary>
        /// <param name="blog_id"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        [XmlRpcMethod("wp.newCategory")]
        int NewCategory(string blog_id, string username, string password, WpCategory category);
    }
}