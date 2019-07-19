namespace iBlogs.Site.Core.Install.DTO
{
    public class InstallParam
    {
        public string SiteTitle { get; set; }
        public string SiteUrl { get; set; }
        public string AdminUser { get; set; }
        public string AdminEmail { get; set; }
        public string AdminPwd { get; set; }
        public string DbUrl { get; set; }
        public string DbName { get; set; }
        public string DbUserName { get; set; }
        public string DbUserPwd { get; set; }
    }
}