using System.Collections.Generic;
using iBlogs.Site.MetaWeblog.Classes;

namespace iBlogs.Site.MetaWeblog.Wrappers
{
    public interface IMetaWeblogWrapper
    {
        bool DeletePost(int postid);        
        IEnumerable<Category> GetCategories();
        Post GetPost(int postID);
        IEnumerable<Post> GetRecentPosts(int numberOfPosts);
        IEnumerable<UserBlog> GetUserBlogs();
        UserInfo GetUserInfo();
        MediaObjectInfo NewMediaObject(MediaObject mediaObject);
        int NewPost(Post post, bool publish);
    }
}