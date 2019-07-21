using iBlogs.Site.MetaWeblog.Classes;
using iBlogs.Site.MetaWeblog.Wrappers;
using NUnit.Framework;

namespace iBlogs.Site.MetaWeblog.Test
{
    [Ignore("未完..")]
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
        public void GetUsersBlogs()
        {
            var result = clinet.GetUsersBlogs();
            Assert.NotNull(result);
        }
        [Test]
        public void GetRecentPosts()
        {
            var result = clinet.GetRecentPosts(int.MaxValue);
            Assert.NotNull(result);
        }
        [Test]
        public void GetCategories()
        {
            var result = clinet.GetCategories();
            Assert.NotNull(result);
        }

        [Test]
        public void NewPost()
        {
            var result = clinet.NewPost(new Post(), false);
        }
        [Test]
        public void GetPost()
        {
            var result = clinet.GetPost("123");
        }
        [Test]
        public void EditPost()
        {
            var result = clinet.EditPost("123",new Post(), false);
        }
        [Test]
        public void DeletePost()
        {
            var result = clinet.DeletePost("123", false);
        }
        [Test]
        public void NewCategory()
        {
            var result = clinet.NewCategory(new WpCategory());
        }
    }
}