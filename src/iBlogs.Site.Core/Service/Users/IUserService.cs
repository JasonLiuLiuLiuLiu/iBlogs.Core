using System.Collections.Generic;

namespace iBlogs.Site.Core.Service.Users
{
    public interface IUserService
    {
        Entity.Users CurrentUsers { get; set; }
        bool InsertUser(Entity.Users user);
        List<Entity.Users> FindUsers(Entity.Users user);
    }
}