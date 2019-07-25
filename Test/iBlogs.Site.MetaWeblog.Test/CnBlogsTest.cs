using System;
using System.Linq;
using iBlogs.Site.MetaWeblog.Classes;
using iBlogs.Site.MetaWeblog.Wrappers;
using NUnit.Framework;

namespace iBlogs.Site.MetaWeblog.Test
{
    [TestFixture]
    public class CnBlogsTest
    {
        private ICnBlogsWrapper _client;
        [SetUp]
        public void Setup()
        {
            _client = new CnBlogsLoginInfo().GetCnBlogsClient();
        }
        [Test]
        public void GetUsersBlogs()
        {
            var result = _client.GetUsersBlogs().FirstOrDefault();
            Assert.NotNull(result);
            Assert.AreEqual(result.BlogId, "529635");
        }
        [Test]
        public void GetRecentPosts()
        {
            var result = _client.GetRecentPosts(int.MaxValue);
            Assert.NotNull(result);
        }
        [Test]
        public void GetCategories()
        {
            var result = _client.GetCategories();
            Assert.NotNull(result);
        }

        [Test]
        public void NewGetAndDeletePost()
        {
            var post = new Post
            {
                DateCreated = DateTime.Now,
                Description = "This is a test post",
                Title = "This is a title"
            };
            var postId = _client.NewPost(post, false);
            Assert.NotNull(postId);

            var postResult = _client.GetPost(postId);
            Assert.AreEqual(postResult.Title, post.Title);



            var editResult = _client.EditPost(postId, postResult, false);
            Assert.NotNull(editResult);

            var deleted = _client.DeletePost(postId, false);
            Assert.True(deleted);
        }
    }
}