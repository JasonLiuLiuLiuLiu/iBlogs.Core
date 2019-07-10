using System;
using System.Collections.Generic;
using System.Text;

namespace iBlogs.Site.Core.User.DTO
{
    public class PwdUpdateParam
    {
        public string OldPassword { get; set; }
        public string Password { get; set; }
    }
}
