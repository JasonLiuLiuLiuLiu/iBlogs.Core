using CookComputing.XmlRpc;
using iBlogs.Site.MetaWeblog.Classes;
using iBlogs.Site.MetaWeblog.CnBlogs;
using iBlogs.Site.MetaWeblog.CnBlogs.XmlRpcStructs;
using Post = iBlogs.Site.MetaWeblog.Classes.Post;

namespace iBlogs.Site.MetaWeblog.Wrappers
{
    public class CnBlogsWrapper : MetaWeblogWrapper, ICnBlogsWrapper
    {
        protected new ICnBlogsXmlRpc Wrapper;
        public CnBlogsWrapper(string url, string username, string password) : this(url, username, password, 0)
        {
        }

        public CnBlogsWrapper(string url, string username, string password, int blogID) : base(url, username, password, blogID)
        {
            Wrapper = (ICnBlogsXmlRpc)XmlRpcProxyGen.Create(typeof(ICnBlogsXmlRpc));
            Wrapper.Url = url;
        }


        public bool DeletePost(string appKey, string postid, string username, string password, bool publish)
        {
            throw new System.NotImplementedException();
        }

        public Post[] GetUsersBlogs(string appKey, string username, string password)
        {
            throw new System.NotImplementedException();
        }

        public int EditPost(string postid, string username, string password, Post post, bool publish)
        {
            throw new System.NotImplementedException();
        }

        public Category[] GetCategories(string blogid, string username, string password)
        {
            throw new System.NotImplementedException();
        }

        public Post GetPost(string postid, string username, string password)
        {
            throw new System.NotImplementedException();
        }

        public Post[] GetRecentPosts(string blogid, string username, string password, int numberOfPosts)
        {
            throw new System.NotImplementedException();
        }

        public string NewMediaObject(string blogid, string username, string password, File file)
        {
            throw new System.NotImplementedException();
        }

        public string NewPost(string blogid, string username, string password, Post post, bool publish)
        {
            throw new System.NotImplementedException();
        }

        public int NewCategory(string blog_id, string username, string password, Category category)
        {
            throw new System.NotImplementedException();
        }
    }
}
