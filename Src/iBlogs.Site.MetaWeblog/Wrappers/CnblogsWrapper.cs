using System;
using System.Linq;
using CookComputing.XmlRpc;
using iBlogs.Site.MetaWeblog.Classes;
using iBlogs.Site.MetaWeblog.CnBlogs;
using iBlogs.Site.MetaWeblog.Helpers;

namespace iBlogs.Site.MetaWeblog.Wrappers
{
    public class CnBlogsWrapper : MetaWeblogWrapper, ICnBlogsWrapper
    {
        protected new ICnBlogsXmlRpc Wrapper;

        private string _appKey = "appKey";

        public CnBlogsWrapper(string url, string username, string password) : this(url, username, password, 0)
        {
            BlogID = int.Parse(GetUsersBlogs().FirstOrDefault()?.BlogId ?? throw new InvalidOperationException());
        }

        public CnBlogsWrapper(string url, string username, string password, int blogId) : base(url, username, password, blogId)
        {
            Init();
        }

        private void Init()
        {
            Wrapper = (ICnBlogsXmlRpc)XmlRpcProxyGen.Create(typeof(ICnBlogsXmlRpc));
            Wrapper.Url = Url;
        }

        public BlogInfo[] GetUsersBlogs()
        {
            var xmlRpcArray = Wrapper.GetUsersBlogs(_appKey, Username, Password);
            if (xmlRpcArray == null)
                return null;

            var resultArray = new BlogInfo[xmlRpcArray.Length];
            for (int i = 0; i < xmlRpcArray.Length; i++)
            {
                resultArray[i] = Mapper.From.BlogInfo(xmlRpcArray[i]);
            }
            return resultArray;
        }

        public Post[] GetRecentPosts(int numberOfPosts)
        {
            var xmlRpcArray = Wrapper.GetRecentPosts(BlogID.ToString(), Username, Password, numberOfPosts);
            if (xmlRpcArray == null)
                return null;
            var resultArray = new Post[xmlRpcArray.Length];
            for (int i = 0; i < xmlRpcArray.Length; i++)
            {
                resultArray[i] = Mapper.From.Post(xmlRpcArray[i]);
            }
            return resultArray;
        }

        public bool DeletePost(string postId, bool publish)
        {
            return Wrapper.DeletePost(_appKey, postId, Username, Password, publish);
        }

        public Post GetPost(string postId)
        {
            return Mapper.From.Post(Wrapper.GetPost(postId, Username, Password));
        }

        public object EditPost(string postId, Post post, bool publish)
        {
            return Wrapper.EditPost(postId, Username, Password, Mapper.To.Post(post), publish);
        }

        public CategoryInfo[] GetCategories()
        {
            var xmlRpcArray = Wrapper.GetCategories(BlogID.ToString(), Username, Password);
            if (xmlRpcArray == null)
                return null;
            var resultArray=new CategoryInfo[xmlRpcArray.Length];
            for (int i = 0; i < xmlRpcArray.Length; i++)
            {
                resultArray[i] = Mapper.From.CategoryInfo(xmlRpcArray[i]);
            }
            return resultArray;
        }

        public string NewPost(Post post, bool publish)
        {
            return Wrapper.NewPost(BlogID.ToString(), Username, Password, Mapper.To.Post(post), publish);
        }

        public int NewCategory(WpCategory category)
        {
            return Wrapper.NewCategory(BlogID.ToString(), Username, Password, Mapper.To.WpCategory(category));
        }
    }
}
