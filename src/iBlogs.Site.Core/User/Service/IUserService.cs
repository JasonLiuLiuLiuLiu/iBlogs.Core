using System.Collections.Generic;
using iBlogs.Site.Core.User.DTO;

namespace iBlogs.Site.Core.User.Service
{
    public interface IUserService
    {
        CurrentUser CurrentUsers { get; set; }
        bool InsertUser(Users user);
        List<Users> FindUsers(Users user);
    }
}