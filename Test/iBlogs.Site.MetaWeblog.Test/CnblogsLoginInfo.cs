using iBlogs.Site.MetaWeblog.Wrappers;

namespace iBlogs.Site.MetaWeblog.Test
{
    public class CnBlogsLoginInfo
    {

        public string Username = "iblogs.site";
        public string XmlRpcUrl = "https://rpc.cnblogs.com/metaweblog/iblogssite";
        public string Password = "123asd!@#";

        public ICnBlogsWrapper GetCnBlogsClient()
        {
            var result = new CnBlogsWrapper(XmlRpcUrl, Username, Password);
            return result;
        }

        public override string ToString()
        {
            return $"{XmlRpcUrl} {Username} {Password}";
        }
    }
}
