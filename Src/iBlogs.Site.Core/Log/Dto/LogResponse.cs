using System;

namespace iBlogs.Site.Core.Log.Dto
{
    public class LogResponse
    {
        public int Id { get; set; }

        public DateTime Timestamp { get; set; }

        public string Level { get; set; }

        public string Template { get; set; }

        public string Message { get; set; }

        public string Exception { get; set; }

        public string Properties { get; set; }

    }
}
