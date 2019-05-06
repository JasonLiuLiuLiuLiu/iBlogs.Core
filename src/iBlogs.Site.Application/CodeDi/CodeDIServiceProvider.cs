using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace iBlogs.Site.Application.CodeDi
{
    public class CodeDiServiceProvider : ICodeDiServiceProvider
    {
        private readonly IServiceProvider _serviceProvider;
        public CodeDiServiceProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public T GetService<T>() where T : class
        {
            return _serviceProvider.GetService<IEnumerable<T>>().FirstOrDefault();
        }

        public T GetService<T>(string name) where T : class
        {
            return _serviceProvider.GetService<IEnumerable<T>>().FirstOrDefault(u => u.GetType().Name.Matches( name));
        }
    }
}
