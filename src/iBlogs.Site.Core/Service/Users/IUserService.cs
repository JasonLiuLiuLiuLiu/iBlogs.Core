using System.Collections.Generic;
using iBlogs.Site.Core.Dto;

namespace iBlogs.Site.Core.Service.Users
{
    public interface IUserService
    {
        CurrentUser CurrentUsers { get; set; }
        bool InsertUser(Entity.Users user);
        List<Entity.Users> FindUsers(Entity.Users user);
    }
}