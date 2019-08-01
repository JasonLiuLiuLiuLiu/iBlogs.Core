using CookComputing.XmlRpc;
using iBlogs.Site.MetaWeblog.XmlRpcInterfaces;

namespace iBlogs.Site.MetaWeblog.Wrappers
{
    /// <summary>
    ///Implements the MetaWeblog API
    ///http://msdn.microsoft.com/en-us/library/bb259697.aspx
    /// </summary>
    public class MetaWeblogWrapper : BaseWrapper, IMetaWeblogWrapper
    {
        protected IMetaWeblogXmlRpc Wrapper;

        public MetaWeblogWrapper(string url, string username, string password)
            : this(url, username, password, 0)
        {
            Wrapper = (IMetaWeblogXmlRpc)XmlRpcProxyGen.Create(typeof(IMetaWeblogXmlRpc));
            Wrapper.Url = Url;
        }

        public MetaWeblogWrapper(string url, string username, string password, int blogID)
            : base(url, username, password, blogID)
        {
            Wrapper = (IMetaWeblogXmlRpc)XmlRpcProxyGen.Create(typeof(IMetaWeblogXmlRpc));
            Wrapper.Url = Url;
        }
       

        public override void Dispose()
        {
            Wrapper = null;
        }
    }
}