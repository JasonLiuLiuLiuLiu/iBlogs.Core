using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace iBlogs.Site.Core.EntityFrameworkCore
{
    public class iBlogsContext:DbContext
    {
        public iBlogsContext(DbContextOptions<iBlogsContext> options) : base(options)
        {

        }
    }
}
