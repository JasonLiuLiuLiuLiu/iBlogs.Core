namespace iBlogs.Site.Core.MailKit
{
    public interface IMailService
    {
        void Publish(MailContext context);
        void SendCapSubscription(MailContext context);
    }
}
