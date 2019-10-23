using System;
using System.Collections.Concurrent;

namespace iBlogs.Site.Core.Storage
{
    internal class StorageWarehouse
    {
        private static readonly ConcurrentDictionary<Type, object> Warehouse;
        static StorageWarehouse()
        {
            Warehouse = new ConcurrentDictionary<Type, object>();
        }

        public static ConcurrentDictionary<int,T> Get<T>() where T : class, IEntityBase
        {
            if (Warehouse.ContainsKey(typeof(T)))
                return (ConcurrentDictionary<int, T>)Warehouse[typeof(T)];
            return new ConcurrentDictionary<int, T>();
        }

        public static void Set<T>(ConcurrentDictionary<int, T> values)
        {
            if (values == null) return;

            if (Warehouse.ContainsKey(typeof(T)))
            {
                Warehouse[typeof(T)] = values;
            }
        }
    }
}