using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using iBlogs.Site.Core.Common.Extensions;
using iBlogs.Site.Core.SqLite;
using iBlogs.Site.Core.User.DTO;

namespace iBlogs.Site.Core.User.Service
{
    public class UserService : IUserService
    {
        private readonly DbConnection _sqLite;

        public UserService(IDbBaseRepository db)
        {
            _sqLite = db.DbConnection();
        }

        public CurrentUser CurrentUsers { get; set; }=new CurrentUser();

        public bool InsertUser(Users user)
        {
            user.PwdMd5();
            return _sqLite.Execute("INSERT into t_users (username,password,email,created) VALUES (@Username,@Password,@Email,@Created)", user) == 1;
        }

        public List<Users> FindUsers(Users user)
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

            return _sqLite.Query<Users>(sqlBuilder.ToString(), user).ToList();
        }
    }
}
