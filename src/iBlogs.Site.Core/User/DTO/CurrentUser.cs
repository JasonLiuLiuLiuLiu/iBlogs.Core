namespace iBlogs.Site.Core.User.DTO
{
    public class CurrentUser
    {
        /**
     * user表主键
     */
        public int Uid { get; set; }

        /**
         * 用户名称
         */
        public string Username { get; set; }

       
        /**
         * 用户的邮箱
         */
        public string Email { get; set; }


        /**
         * 用户显示的名称
         */
        public string ScreenName { get; set; }

        /**
        * 用户的主页
        */
        public string HomeUrl { get; set; }
    }
}
