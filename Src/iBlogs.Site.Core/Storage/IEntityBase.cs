using System;

namespace iBlogs.Site.Core.Storage
{
    public interface IEntityBase
    {
        int Id { get; set; }
        DateTime Created { get; set; }
        bool Deleted { get; set; }
    }
}