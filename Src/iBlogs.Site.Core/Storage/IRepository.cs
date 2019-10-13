using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using iBlogs.Site.Core.Common.Request;
using iBlogs.Site.Core.Common.Response;
using Microsoft.EntityFrameworkCore;

namespace iBlogs.Site.Core.Storage
{
    public interface IRepository<TEntity> where TEntity : class, IEntityBase
    {
        DbConnection Connection { get; }

        IQueryable<TEntity> GetAll();

        IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] propertySelectors);

        Task<List<TEntity>> GetAllListAsync();

        Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> FirstOrDefaultAsync(int id);

        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        TEntity Insert(TEntity entity);

        Task<TEntity> InsertAsync(TEntity entity);

        int InsertAndGetId(TEntity entity);

        Task<int> InsertAndGetIdAsync(TEntity entity);

        int InsertOrUpdateAndGetId(TEntity entity);

        Task<int> InsertOrUpdateAndGetIdAsync(TEntity entity);

        TEntity Update(TEntity entity);

        Task<TEntity> UpdateAsync(TEntity entity);

        void Delete(TEntity entity);

        void Delete(int id);

        Task<int> CountAsync();

        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate);

        Task<long> LongCountAsync();

        Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate);

        DbContext GetDbContext();

        Task EnsureCollectionLoadedAsync<TProperty>(
            TEntity entity,
            Expression<Func<TEntity, IEnumerable<TProperty>>> collectionExpression,
            CancellationToken cancellationToken)
            where TProperty : class;

        Task EnsurePropertyLoadedAsync<TProperty>(
            TEntity entity,
            Expression<Func<TEntity, TProperty>> propertyExpression,
            CancellationToken cancellationToken)
            where TProperty : class;

        TEntity InsertOrUpdate(TEntity entity);

        Task<TEntity> InsertOrUpdateAsync(TEntity entity);

        TEntity FirstOrDefault(int id);

        Page<TEntity> Page(IQueryable<TEntity> source, PageParam pageParam);

        void SaveChanges();
    }
}