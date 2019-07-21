using System;
using System.Collections.Generic;
using System.Text;
using iBlogs.Site.MetaWeblog.Wrappers;

namespace iBlogs.Site.MetaWeblog.Test
{
    public class CnBlogsLoginInfo
    {
        public string XmlRpcUrl = "https://www.cnblogs.com/coderayu/services/metaweblog.aspx";
        public string Username = "Your UserName";
        public string Password = "Your Password";

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
