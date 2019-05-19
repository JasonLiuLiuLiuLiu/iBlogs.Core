using System;
using System.Collections.Generic;
using System.Text;
using iBlogs.Site.Core.Entity;
using Z.Dapper.Plus;

namespace iBlogs.Site.Core.Utils
{
    public class DapperPlus
    {
        public static void SetMapping()
        {
            DapperPlusManager.Entity<Attach>().Table("t_attach").Identity(x => x.Id);
            DapperPlusManager.Entity<Comments>().Table("t_comments").Identity(x => x.Cid);
            DapperPlusManager.Entity<Contents>().Table("t_contents").Identity(x => x.Cid);
            DapperPlusManager.Entity<Logs>().Table("t_logs").Identity(x => x.Id);
            DapperPlusManager.Entity<Metas>().Table("t_metas").Identity(x => x.Mid);
            DapperPlusManager.Entity<Options>().Table("t_options");
            DapperPlusManager.Entity<Relationships>().Table("t_relationship");
            DapperPlusManager.Entity<Users>().Table("t_users").Identity(x => x.Uid);
        }
    }
}
