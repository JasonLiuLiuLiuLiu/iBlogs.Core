using iBlogs.Site.MetaWeblog.Classes;

namespace iBlogs.Site.MetaWeblog.Wrappers
{
    public interface ICnBlogsWrapper:IMetaWeblogWrapper
    {
        /// <summary>
        /// Deletes a post.
        /// </summary>
        /// <param name="appKey"></param>
        /// <param name="postId"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="publish">Where applicable, this specifies whether the blog should be republished after the post has been deleted.</param>
        /// <returns>Always returns true.</returns>
        bool DeletePost(string appKey, string postId, string username, string password, bool publish);
        /// <summary>
        /// Returns information on all the blogs a given user is a member.
        /// </summary>
        /// <returns></returns>
        BlogInfo[] GetUsersBlogs();
        /// <summary>
        /// Updates and existing post to a designated blog using the metaWeblog API. Returns true if completed.
        /// </summary>
        /// <param name="postId"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="post"></param>
        /// <param name="publish"></param>
        /// <returns></returns>
        int EditPost(string postId, string username, string password, Post post, bool publish);
        /// <summary>
        /// Retrieves a list of valid categories for a post using the metaWeblog API. 
        /// </summary>
        /// <param name="blogId"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        WpCategory GetCategories(string blogId, string username, string password);
        /// <summary>
        /// Retrieves an existing post using the metaWeblog API. 
        /// </summary>
        /// <param name="postId"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Post GetPost(string postId, string username, string password);
        /// <summary>
        /// Retrieves a list of the most recent existing post using the metaWeblog API. 
        /// </summary>
        /// <param name="blogId"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="numberOfPosts"></param>
        /// <returns></returns>
        Post[] GetRecentPosts(string blogId, string username, string password, int numberOfPosts);

        /// <summary>
        /// Makes a new post to a designated blog using the metaWeblog API. 
        /// </summary>
        /// <param name="blogId"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="post"></param>
        /// <param name="publish"></param>
        /// <returns></returns>
        string NewPost(string blogId, string username, string password, Post post, bool publish);
    }
}