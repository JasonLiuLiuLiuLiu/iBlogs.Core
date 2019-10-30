using System;
using iBlogs.Site.Core.Common;
using iBlogs.Site.Core.Common.Extensions;
using iBlogs.Site.Core.Common.Request;
using iBlogs.Site.Core.Common.Response;
using iBlogs.Site.Core.Log.Dto;
using iBlogs.Site.Core.MailKit;
using iBlogs.Site.Core.Option;
using Newtonsoft.Json;
using Serilog.Events;

namespace iBlogs.Site.Core.Log.Service
{
    public class LogService : ILogService
    {

        private static IMailService _mailService;

        public static void ErrorLogEventCallBack(LogEvent logEvent)
        {
            try
            {
                if (_mailService == null)
                {
                    _mailService = ServiceFactory.GetService<IMailService>();
                }

                var adminEmail = ConfigData.Get(ConfigKey.AdminEmail);

                if (adminEmail.IsNullOrEmpty()) return;

                _mailService.Publish(new MailContext
                {
                    To = new[] { adminEmail },
                    Subject = "错误日志告警",
                    Content = JsonConvert.SerializeObject(logEvent)
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }

        public Page<LogResponse> GetPage(PageParam page)
        {
            throw new NotImplementedException();
        }
    }
}