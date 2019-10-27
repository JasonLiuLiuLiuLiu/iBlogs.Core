using System;
using System.Collections.Concurrent;
using iBlogs.Site.Core.Blog.Attach;
using iBlogs.Site.Core.Blog.Comment;
using iBlogs.Site.Core.Blog.Content;
using iBlogs.Site.Core.Blog.Extension;
using iBlogs.Site.Core.Blog.Meta;
using iBlogs.Site.Core.Blog.Relationship;
using iBlogs.Site.Core.Security;

namespace iBlogs.Site.Core.Storage
{
    internal class StorageWarehouse
    {
        private static readonly ConcurrentDictionary<Type, object> Warehouse;
        static StorageWarehouse()
        {
            Warehouse = new ConcurrentDictionary<Type, object>();
        }

        public static ConcurrentDictionary<int, T> Get<T>() where T : class, IEntityBase
        {
            return (ConcurrentDictionary<int, T>)Warehouse.GetOrAdd(typeof(T), key => new ConcurrentDictionary<int, T>());
        }

        public static void Set<T>(ConcurrentDictionary<int, T> values)
        {
            if (values == null) return;

            if (!Warehouse.ContainsKey(typeof(T)))
            {
                Warehouse.TryAdd(typeof(T), values);
            }
            else
            {
                Warehouse[typeof(T)] = values;
            }
        }

        public static void BuildRelationShip()
        {
            BlogSyncRelationship2Content();
            Comment2Content();
            Content2User();
            Attachments2User();
            Relationship2ContentAndMeta();
        }

        private static void BlogSyncRelationship2Content()
        {
            var blogsSyncRelationships = Get<BlogSyncRelationship>();
            var contents = Get<Contents>();
            foreach (var relationship in blogsSyncRelationships)
            {
                relationship.Value.Content = GetEntityOrDefault(relationship.Value.ContentId, contents);
            }
        }

        private static void Comment2Content()
        {
            var contents = Get<Contents>();
            var comments = Get<Comments>();
            foreach (var comment in comments)
            {
                comment.Value.Article = GetEntityOrDefault(comment.Key, contents);
            }
        }

        private static void Content2User()
        {
            var contents = Get<Contents>();
            var users = Get<Users>();
            foreach (var content in contents)
            {
                content.Value.Author = GetEntityOrDefault(content.Value.AuthorId, users);
            }
        }

        private static void Attachments2User()
        {
            var attachments = Get<Attachment>();
            var users = Get<Users>();
            foreach (var attachment in attachments)
            {
                attachment.Value.Author = GetEntityOrDefault(attachment.Value.AuthorId, users);
            }
        }

        private static void Relationship2ContentAndMeta()
        {
            var relationships = Get<Relationships>();
            var contents = Get<Contents>();
            var metas = Get<Metas>();
            foreach (var relationship in relationships)
            {
                relationship.Value.Content = GetEntityOrDefault(relationship.Value.Cid, contents);
                relationship.Value.Meta = GetEntityOrDefault(relationship.Value.Mid, metas);
            }
        }

        private static T GetEntityOrDefault<T>(int id, ConcurrentDictionary<int, T> values)
        {
            return values.ContainsKey(id) ? values[id] : default;
        }
    }
}