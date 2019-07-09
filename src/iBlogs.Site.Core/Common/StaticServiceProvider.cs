using System;
using System.Collections.Generic;
using System.Text;

namespace iBlogs.Site.Core.Common
{
    public static class StaticServiceProvider
    {
        private static IServiceProvider _serviceProvider;

        public static void Init(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public static T GetService<T>()
        {
            if (_serviceProvider == null)
                throw new Exception("StaticServiceProvider使用前未进行初始化");

            return (T)_serviceProvider.GetService(typeof(T));
        }
    }
}
