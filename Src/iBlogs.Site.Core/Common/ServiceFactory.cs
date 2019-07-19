using System;
using Microsoft.Extensions.DependencyInjection;

namespace iBlogs.Site.Core.Common
{
    public static class ServiceFactory
    {
        public static IServiceCollection Services { get; set; }
        private static IServiceProvider _provider;
        private static readonly object Lock = new object();
        private static bool _disposed;

        public static T GetService<T>()
        {
            if (_provider == null)
                CreateServiceProvider();
            try
            {
                var result = (T)_provider.GetService(typeof(T));
                if (result != null)
                    return result;
                CreateServiceProvider();
                return (T)_provider.GetService(typeof(T));
            }
            catch
            {
                CreateServiceProvider();
                return (T)_provider.GetService(typeof(T));
            }
        }

        private static void CreateServiceProvider()
        {
            _disposed = true;
            lock (Lock)
            {
                if (!_disposed)
                    return;
                _provider = Services.BuildServiceProvider();
                _disposed = false;
            }
        }
    }
}
