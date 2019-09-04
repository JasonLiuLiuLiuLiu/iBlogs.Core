using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iBlogs.Site.Core.Common.Extensions;
using iBlogs.Site.Core.EntityFrameworkCore;
using iBlogs.Site.Core.Option;
using iBlogs.Site.Core.Option.Service;
using iBlogs.Site.Core.Security.DTO;

namespace iBlogs.Site.Core.Security.Service
{
    public class UserService : IUserService
    {
        private readonly IRepository<Users> _repository;
        private readonly IOptionService _optionService;

        public UserService( IRepository<Users> repository, IOptionService optionService)
        {
            _repository = repository;
            _optionService = optionService;
        }

        public CurrentUser CurrentUsers { get; set; }

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

        public void UpdateUserInfo(UpdateUserParam param)
        {
            var user = _repository.FirstOrDefault(CurrentUsers.Uid);
            user.ScreenName = param.ScreenName;
            user.Email = param.Email;
            _optionService.Set(ConfigKey.AdminEmail,user.Email);
            _repository.SaveChanges();
        }

        public void UpdatePwd(PwdUpdateParam param)
        {
            var pwd = Users.PwdMd5(CurrentUsers.Username, param.OldPassword);
            var user = _repository.FirstOrDefault(CurrentUsers.Uid);
            if (user.Password != pwd)
                throw new Exception("输入密码有误");
            user.Password = param.Password;
            user.PwdMd5();
            LoginToken.RemoveToken(user.Id);
            _repository.SaveChanges();
        }

        public Users FindUserById(int id)
        {
            return _repository.FirstOrDefault(id);
        }
    }
}
