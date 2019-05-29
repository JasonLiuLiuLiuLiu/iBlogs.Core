using System;
using System.Collections.Generic;
using System.Text;

namespace iBlogs.Site.Core.User.Service
{
    public static class LoginStaticToken
    {
        private static Dictionary<int,string> tokenDic=new Dictionary<int, string>();

        public static bool CheckToken(int uid,string token)
        {
            if (tokenDic.ContainsKey(uid) && tokenDic[uid] == token)
                return true;
            return false;
        }

        public static void SaveToken(int uid, string token)
        {
            if (tokenDic.ContainsKey(uid))
            {
                tokenDic[uid] = token;
            }
            else
            {
                tokenDic.Add(uid, token);
            }
        }

        public static void RemoveToken(int uid)
        {
            tokenDic.Remove(uid);
        }
    }
}
