using System;

namespace iBlogs.Site.Core.Entity
{
    public class Logs : EntityBase
    {

        /**
         * 产生的动作
         */
        public string Action { get; set; }

        /**
         * 产生的数据
         */
        public string Data { get; set; }

        /**
         * 日志产生的ip
         */
        public string Ip { get; set; }

        public Logs(string action, string data, string ip, int uid)
        {
            this.Action = action;
            this.Data = data;
            this.Ip = ip;
            this.AuthorId = uid;
            this.Created = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        }
    }
}
