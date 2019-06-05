using System;

namespace iBlogs.Site.Core.EntityFrameworkCore
{
    public interface IEntityBase
    {
        int Id { get; set; }
        DateTime Created { get; set; }
    }
}