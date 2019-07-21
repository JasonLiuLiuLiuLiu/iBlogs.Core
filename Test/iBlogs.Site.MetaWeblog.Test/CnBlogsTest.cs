using iBlogs.Site.MetaWeblog.Wrappers;
using NUnit.Framework;

namespace iBlogs.Site.MetaWeblog.Test
{
    [Ignore("Ignore a test")]
    [TestFixture]
    public class CnBlogsTest
    {
        private ICnBlogsWrapper clinet;
        [SetUp]
        public void Setup()
        {
            clinet=new CnBlogsLoginInfo().GetCnBlogsClient();
        }
        
        [Test]
        public void GetAllBlogInfo()
        {
           var result=clinet.GetUsersBlogs();
           Assert.NotNull(result);
        }
        
        [Test]
        public void GetPosts()
        {
            var result = clinet.GetRecentPosts(int.MaxValue);
            Assert.NotNull(result);
        }
    }
}