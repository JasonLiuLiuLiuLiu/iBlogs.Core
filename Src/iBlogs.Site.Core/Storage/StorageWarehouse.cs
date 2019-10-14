using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace iBlogs.Site.Core.Storage
{
    internal class StorageWarehouse
    {
        private static readonly ConcurrentDictionary<Type, IEnumerable<object>> Warehouse;
        static StorageWarehouse()
        {
            Warehouse = new ConcurrentDictionary<Type, IEnumerable<object>>();
        }

        public static IEnumerable<T> Get<T>()
        {
            if (Warehouse.ContainsKey(typeof(T)))
                return Warehouse[typeof(T)].Select(u => (T)u);
            return new T[0];
        }

        public static void Set<T>(IEnumerable<T> values)
        {
            if (values == null) return;

            if (Warehouse.ContainsKey(typeof(T)))
            {
                Warehouse[typeof(T)] = values.Select(u => (object)u);
            }
        }
    }
}