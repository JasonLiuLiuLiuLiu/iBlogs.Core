using System;
using System.Collections.Generic;
using iBlogs.Site.Core.Blog.Extension;
using iBlogs.Site.Core.Common;
using iBlogs.Site.Core.Git;
using iBlogs.Site.Core.Startup.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace iBlogs.Site.Core.Startup
{
    public class BlogsStartupFilter : IStartupFilter
    {
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return async app =>
            {
                var appLifetime = ServiceFactory.GetService<IHostApplicationLifetime>();
                var blogSyncExtenstion = ServiceFactory.GetService<IEnumerable<IBlogsSyncExtension>>();
                foreach (var extension in blogSyncExtenstion)
                {
                    await extension.InitializeSync().ConfigureAwait(false);
                }

                appLifetime.ApplicationStarted.Register(() => { Console.WriteLine("iBlogs started."); });

                appLifetime.ApplicationStopping.Register(() =>
                {
                    Console.WriteLine("iBlogs is stopping");
                    GitDataSyncHelper.DataSync();
                });

                appLifetime.ApplicationStopped.Register(() => { Console.WriteLine("iBlogs stopped."); });

                app.UseMiddleware<JwtInHeaderMiddleware>();

                next(app);
            };
        }
    }
}
