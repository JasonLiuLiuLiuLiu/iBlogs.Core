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
               LogService.ErrorLogEventCallBack(logEvent);
            }
        }
    }
}
