using System;
using Microsoft.Extensions.DependencyInjection;

namespace iBlogs.Site.Core.Utils.CodeDi
{
    public static class CodeDi
    {
        public static IServiceCollection AddCoreDi(this IServiceCollection services, CodeDiOptions options = null)
        {
            return new CodeDiService(services, options).AddService();
        }

        public static IServiceCollection AddCoreDi(this IServiceCollection services, Action<CodeDiOptions> actionOptions)
        {
            CodeDiOptions options = new CodeDiOptions();
            actionOptions(options);
            return new CodeDiService(services, options).AddService();
        }
    }
}
