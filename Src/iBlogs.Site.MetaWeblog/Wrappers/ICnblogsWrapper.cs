
using iBlogs.Site.MetaWeblog.Classes;

namespace iBlogs.Site.MetaWeblog.Wrappers
{
    public interface ICnBlogsWrapper:IMetaWeblogWrapper
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
        bool DeletePost(string appKey, string postid, string username, string password, bool publish);
        /// <summary>
        /// Returns information on all the blogs a given user is a member.
        /// </summary>
        /// <param name="appKey"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Post[] GetUsersBlogs(string appKey, string username, string password);
        /// <summary>
        /// Updates and existing post to a designated blog using the metaWeblog API. Returns true if completed.
        /// </summary>
        /// <param name="postid"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="post"></param>
        /// <param name="publish"></param>
        /// <returns></returns>
        int EditPost(string postid, string username, string password, Post post, bool publish);
        /// <summary>
        /// Retrieves a list of valid categories for a post using the metaWeblog API. Returns the metaWeblog categories struct collection.
        /// </summary>
        /// <param name="blogid"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Category[] GetCategories(string blogid, string username, string password);
        /// <summary>
        /// Retrieves an existing post using the metaWeblog API. Returns the metaWeblog struct.
        /// </summary>
        /// <param name="postid"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Post GetPost(string postid, string username, string password);
        /// <summary>
        /// Retrieves a list of the most recent existing post using the metaWeblog API. Returns the metaWeblog struct collection.
        /// </summary>
        /// <param name="blogid"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="numberOfPosts"></param>
        /// <returns></returns>
        Post[] GetRecentPosts(string blogid, string username, string password, int numberOfPosts);
        /// <summary>
        /// Makes a new file to a designated blog using the metaWeblog API. Returns url as a string of a struct.
        /// </summary>
        /// <param name="blogid"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        string NewMediaObject(string blogid, string username, string password, File file);
        /// <summary>
        /// Makes a new post to a designated blog using the metaWeblog API. Returns postid as a string.
        /// </summary>
        /// <param name="blogid"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="post"></param>
        /// <param name="publish"></param>
        /// <returns></returns>
        string NewPost(string blogid, string username, string password, Post post, bool publish);
        /// <summary>
        /// Create a new category
        /// </summary>
        /// <param name="blog_id"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        int NewCategory(string blog_id, string username, string password, Category category);
    }
}