namespace iBlogs.Site.Core.MailKit
{
    public interface IMailService
    {
        void Publish(MailContext context);
        void Send(MailContext context);
    }
}
