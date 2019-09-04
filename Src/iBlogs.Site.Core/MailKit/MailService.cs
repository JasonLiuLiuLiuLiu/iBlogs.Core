using System;
using System.Linq;
using DotNetCore.CAP;
using iBlogs.Site.Core.Common.Extensions;
using iBlogs.Site.Core.Option;
using iBlogs.Site.Core.Option.Service;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using MimeKit;

namespace iBlogs.Site.Core.MailKit
{
    public class MailService : IMailService, ICapSubscribe
    {
        private readonly IOptionService _optionService;
        private readonly ICapPublisher _capPublisher;
        private readonly ILogger<MailService> _logger;

        public MailService(IOptionService optionService, ICapPublisher capPublisher, ILogger<MailService> logger)
        {
            _optionService = optionService;
            _capPublisher = capPublisher;
            _logger = logger;
        }

        public void Publish(MailContext context)
        {
            if (!CheckOption(out var errorMessage))
            {
                throw new Exception(errorMessage);
            }

            if (context.To == null || context.To.Length == 0)
            {
                errorMessage = "通知人邮件地址不能为空";
                throw new Exception(errorMessage);
            }

            _capPublisher.Publish("iBlogs.Site.Core.Mail", context);
        }

        [CapSubscribe("iBlogs.Site.Core.Mail")]
        public void SendCapSubscription(MailContext context)
        {
            try
            {
                if (!CheckOption(out string errorMessage))
                {
                    throw new Exception(errorMessage);
                }

                var message = new MimeMessage();
                var fromMail = _optionService.Get(ConfigKey.EmailFromAccount);
                message.From.Add(new MailboxAddress(fromMail, fromMail));
                message.To.AddRange(context.To.Select(t => new MailboxAddress(t, t)));

                if (context.Cc != null)
                {
                    message.Cc.AddRange(context.Cc.Select(t => new MailboxAddress(t, t)));
                }

                if (context.Bcc != null)
                {
                    message.Bcc.AddRange(context.Bcc.Select(t => new MailboxAddress(t, t)));
                }

                message.Subject = context.Subject;

                message.Body = new TextPart("plain")
                {
                    Text = context.Content
                };

                using (var client = new SmtpClient())
                {
                    // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
                    // client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    var host = _optionService.Get(ConfigKey.EmailSmtpHost);
                    var port = int.Parse(_optionService.Get(ConfigKey.EmailSmtpHostPort, "587"));

                    client.Connect(host, port, false);

                    var userName = _optionService.Get(ConfigKey.EmailUserName);
                    var password = _optionService.Get(ConfigKey.EmailPassword);

                    // Note: only needed if the SMTP server requires authentication
                    client.Authenticate(userName, password);

                    client.Send(message);
                    client.Disconnect(true);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw;
            }
        }

        public bool CheckOption(out string message)
        {
            var from = _optionService.Get(ConfigKey.EmailFromAccount);
            var userName = _optionService.Get(ConfigKey.EmailUserName);
            var password = _optionService.Get(ConfigKey.EmailPassword);
            var host = _optionService.Get(ConfigKey.EmailSmtpHost);

            message = "";

            if (from.IsNullOrWhiteSpace())
            {
                message += "请配置邮件From地址,";
            }

            if (userName.IsNullOrWhiteSpace())
            {
                message += "请配置邮件用户名,";
            }

            if (password.IsNullOrWhiteSpace())
            {
                message += "请配置邮件密码,";
            }

            if (host.IsNullOrWhiteSpace())
            {
                message += "请配置邮件服务器地址";
            }

            return message == "";
        }
    }
}
