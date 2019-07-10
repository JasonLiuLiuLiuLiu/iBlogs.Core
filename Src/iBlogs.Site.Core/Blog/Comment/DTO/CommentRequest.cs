namespace iBlogs.Site.Core.Blog.Comment.DTO
{
    public class CommentRequest
    {
        public int? Coid { get; set; }
        public int Cid { get; set; }
        public string Author { get; set; }
        public string Mail { get; set; }
        public string Url { get; set; }
        public string Content { get; set; }
    }
}
