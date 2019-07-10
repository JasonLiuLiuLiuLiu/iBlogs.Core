using System;
using Microsoft.Extensions.DependencyInjection;

namespace iBlogs.Site.Core.Common
{
    public static class ServiceFactory
    {
        public static IServiceCollection Services { get; set; }
        private static IServiceProvider _instance;
        private static readonly object Lock = new object();
        private static bool _disposed;

        public static T GetService<T>()
        {
            try
            {
                return (T)_instance.GetService(typeof(T));
            }
            catch
            {
                _disposed = true;
                CreateServiceProvider();
                return (T)_instance.GetService(typeof(T));
            }


        }

        private static void CreateServiceProvider()
        {
            lock (Lock)
            {
                if (!_disposed)
                    return;
                _instance = Services.BuildServiceProvider();
                _disposed = false;
            }
        }
    }
}
