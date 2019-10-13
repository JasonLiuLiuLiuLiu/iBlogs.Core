using System;
using System.Collections.Generic;
using iBlogs.Site.Core.Blog.Attach;
using iBlogs.Site.Core.Blog.Comment;
using iBlogs.Site.Core.Blog.Content;
using iBlogs.Site.Core.Blog.Extension;
using iBlogs.Site.Core.Blog.Meta;
using iBlogs.Site.Core.Blog.Relationship;
using iBlogs.Site.Core.Option;
using iBlogs.Site.Core.Security;
using Microsoft.EntityFrameworkCore;

namespace iBlogs.Site.Core.Storage
{
    internal class StorageWarehouse
    {
        private static Dictionary<Type, List<object>> _warehouse;
        static StorageWarehouse()
        {
            _warehouse = new Dictionary<Type, List<object>>();
        }
    }
}