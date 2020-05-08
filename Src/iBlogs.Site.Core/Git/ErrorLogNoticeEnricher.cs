using System.Linq;
using iBlogs.Site.Core.Common.Extensions;
using iBlogs.Site.Core.Log.Service;
using Serilog.Core;
using Serilog.Events;

namespace iBlogs.Site.Core.Git
{
    public class ErrorLogNoticeEnricher : ILogEventEnricher
    {
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            if (logEvent.Level >= LogEventLevel.Error)
            {
                if (logEvent.Exception != null && ignoreList.Any(u => logEvent.Exception.Message.Contains(u)))
                    return;
                LogService.ErrorLogEventCallBack(logEvent);
            }
        }

        private string[] ignoreList = new[] { "StatusCode cannot be set because the response has already started." };
    }
}
