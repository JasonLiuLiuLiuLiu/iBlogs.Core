using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using iBlogs.Site.Core.Dto;
using iBlogs.Site.Core.Extensions;
using iBlogs.Site.Core.SqLite;
using Microsoft.Data.Sqlite;

namespace iBlogs.Site.Core.Service.Users
{
    public class UserService : IUserService
    {
        private readonly SqliteConnection _sqLite;

        public UserService(ISqLiteBaseRepository sqLite)
        {
            _sqLite = sqLite.DbConnection();
        }

        public CurrentUser CurrentUsers { get; set; }=new CurrentUser();

        public bool InsertUser(Entity.Users user)
        {
            user.PwdMd5();
            return _sqLite.Execute("INSERT into t_users (username,password,email,created) VALUES (@Username,@Password,@Email,@Created)", user) == 1;
        }

        public List<Entity.Users> FindUsers(Entity.Users user)
        {
            var sqlBuilder = new StringBuilder();
            sqlBuilder.AppendLine("select uid,username,email FROM t_users where 1=1 ");
            if (user.Uid != 0)
                sqlBuilder.Append(" and uid=@Uid ");
            if (!user.Username.IsNullOrWhiteSpace())
                sqlBuilder.Append(" and username=@Username ");
            if (!user.Email.IsNullOrWhiteSpace())
                sqlBuilder.Append(" and email=@Email ");
            if (!user.Password.IsNullOrWhiteSpace())
            {
                user.PwdMd5();
                sqlBuilder.Append(" and password=@Password ");
            }

            return _sqLite.Query<Entity.Users>(sqlBuilder.ToString(), user).ToList();
        }
    }
}
