using CookComputing.XmlRpc;
using iBlogs.Site.MetaWeblog.Classes;
using iBlogs.Site.MetaWeblog.CnBlogs;
using iBlogs.Site.MetaWeblog.Helpers;

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

        public bool DeletePost(string appKey, string postId, string username, string password, bool publish)
        {
            return Wrapper.DeletePost(appKey, postId, username, password, publish);
        }

        public BlogInfo[] GetUsersBlogs()
        {
            var xmlRpcArray = Wrapper.GetUsersBlogs("appKey", Username, Password);
            if (xmlRpcArray == null)
                return null;

            var resultArray = new BlogInfo[xmlRpcArray.Length];
            for (int i = 0; i < xmlRpcArray.Length; i++)
            {
                resultArray[i] = Mapper.From.BlogInfo(xmlRpcArray[i]);
            }
            return resultArray;
        }

        public int EditPost(string postId, string username, string password, Post post, bool publish)
        {
            throw new System.NotImplementedException();
        }

        public WpCategory GetCategories(string blogId, string username, string password)
        {
            throw new System.NotImplementedException();
        }

        public Post GetPost(string postId, string username, string password)
        {
            throw new System.NotImplementedException();
        }

        public Post[] GetRecentPosts(string blogId, string username, string password, int numberOfPosts)
        {
            throw new System.NotImplementedException();
        }

        public string NewPost(string blogId, string username, string password, Post post, bool publish)
        {
            throw new System.NotImplementedException();
        }
    }
}
