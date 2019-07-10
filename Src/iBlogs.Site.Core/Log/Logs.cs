using iBlogs.Site.Core.EntityFrameworkCore;
using iBlogs.Site.Core.User;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iBlogs.Site.Core.Log
{
    public class Logs : IEntityBase
    {
        [Key]
        public int Id { get; set; }

        public int? AuthorId { get; set; }

        [ForeignKey("AuthorId")]
        public Users Author { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime Created { get; set; }

        public bool Deleted { get; set; }

        public string Action { get; set; }
        public string Data { get; set; }
        public string Ip { get; set; }

        public Logs()
        {
        }

        public Logs(string action, string data, string ip, int uid)
        {
            this.Action = action;
            this.Data = data;
            this.Ip = ip;
            this.AuthorId = uid;
            this.Created = DateTime.Now;
        }
    }
}