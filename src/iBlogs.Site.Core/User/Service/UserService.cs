using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using iBlogs.Site.Core.Common.Extensions;
using iBlogs.Site.Core.EntityFrameworkCore;
using iBlogs.Site.Core.User.DTO;

namespace iBlogs.Site.Core.User.Service
{
    public class UserService : IUserService
    {
        private readonly IRepository<Users> _repository;

        public UserService( IRepository<Users> repository)
        {
            _repository = repository;
        }

        public CurrentUser CurrentUsers { get; set; }=new CurrentUser();

        public bool InsertUser(Users user)
        {
            user.PwdMd5();
            return _repository.InsertAndGetId(user) !=0;
        }

        public List<Users> FindUsers(Users user)
        {
            var query = _repository.GetAll();
            var sqlBuilder = new StringBuilder();
            sqlBuilder.AppendLine("select uid,username,email FROM t_users where 1=1 ");
            if (user.Id != 0)
                query = query.Where(u => u.Id == user.Id);
            if (!user.Username.IsNullOrWhiteSpace())
                query = query.Where(u => u.Username == user.Username);
            if (!user.Email.IsNullOrWhiteSpace())
                query = query.Where(u => u.Email == user.Email);
            if (!user.Password.IsNullOrWhiteSpace())
            {
                user.PwdMd5();
                query = query.Where(u => u.Password == user.Password);
            }

            return query.ToList();
        }
    }
}
