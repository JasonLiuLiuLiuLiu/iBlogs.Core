using System.Collections.Generic;

namespace iBlogs.Site.Core.Security.Service
{
    public static class LoginToken
    {
        private static readonly Dictionary<int, string> UserTokens;

        static LoginToken()
        {
            UserTokens = new Dictionary<int, string>();
        }

        public static bool CheckToken(int uid, string token)
        {
            return UserTokens.ContainsKey(uid) && UserTokens[uid] == token;
        }

        public static void SaveToken(int uid, string token)
        {
            if (UserTokens.ContainsKey(uid))
            {
                UserTokens[uid] = token;
            }
            else
            {
                UserTokens.Add(uid, token);
            }
        }

        public static void RemoveToken(int uid)
        {
            UserTokens.Remove(uid);
        }
    }
}
