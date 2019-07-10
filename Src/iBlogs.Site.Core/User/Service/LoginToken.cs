using System;
using System.Collections.Generic;
using System.Text;
using iBlogs.Site.Core.Common;
using iBlogs.Site.Core.Common.Caching;

namespace iBlogs.Site.Core.User.Service
{
    public static class LoginToken
    {
        private const string TokenPre = "IBLOGS_LOGIN_TOKEN_PRE";
        private static readonly int DefaultCacheTime = (int)new TimeSpan(1, 0, 0, 0).TotalMilliseconds;

        private static readonly ICacheManager CacheManager;

        static LoginToken()
        {
            CacheManager = ServiceFactory.GetService<ICacheManager>();
        }

        private static string GetCacheKey(int uid)
        {
            return TokenPre + uid;
        }

        public static bool CheckToken(int uid, string token)
        {
            return CacheManager.Get<string>(GetCacheKey(uid)) == token;
        }

        public static void SaveToken(int uid, string token)
        {
            CacheManager.Set(GetCacheKey(uid), token, DefaultCacheTime);
        }

        public static void RemoveToken(int uid)
        {
            CacheManager.Remove(GetCacheKey(uid));
        }
    }
}
