namespace iBlogs.Site.Core.MailKit
{
    public class MailContext
    {
        public string[] To { get; set; }
        public string[] Cc { get; set; }
        public string[] Bcc { get; set; }
        public string Subject { get; set; } = "iBlogs通知";
        public string Content { get; set; }
    }
}
