using System;
using System.Collections.Generic;
using System.Text;

namespace iBlogs.Site.GitAsDisk
{
    public class SyncResult
    {
        public static SyncResult Success()
        {
            return new SyncResult { Result = true };
        }

        public static SyncResult Failed(string message)
        {
            return new SyncResult
            {
                Result = false,
                Message = message
            };
        }

        public bool Result { get; set; }
        public string Message { get; set; }
    }
}
