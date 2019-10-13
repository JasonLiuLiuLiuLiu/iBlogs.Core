using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using iBlogs.Site.Core.Common.Extensions;
using iBlogs.Site.Core.Common.Request;
using iBlogs.Site.Core.Common.Response;
using Microsoft.EntityFrameworkCore;

namespace iBlogs.Site.Core.Storage
{

    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntityBase
    {
        /// <summary>
        /// Gets EF DbContext object.
        /// </summary>
        private readonly BlogsContext _context;

        /// <summary>
        /// Gets DbSet for given entity.
        /// </summary>
        private DbSet<TEntity> Table => _context.Set<TEntity>();

        public Repository(BlogsContext context)
        {
            _context = context;
        }

        public virtual DbConnection Connection
        {
            get
            {
                var connection = _context.Database.GetDbConnection();

                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                return connection;
            }
        }


        public IQueryable<TEntity> GetAll()
        {
            return GetAllIncluding();
        }

        public IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] propertySelectors)
        {
            var query = Table.AsQueryable();

            if (!propertySelectors.IsNullOrEmpty())
            {
                foreach (var propertySelector in propertySelectors)
                {
                    query = query.Include(propertySelector);
                }
            }

            query = query.Where(u => u.Deleted == false);

            return query;
        }

        public async Task<List<TEntity>> GetAllListAsync()
        {
            return await GetAll().ToListAsync();
        }

        public async Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetAll().Where(predicate).ToListAsync();
        }

        public async Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetAll().SingleAsync(predicate);
        }

        public async Task<TEntity> FirstOrDefaultAsync(int id)
        {
            return await GetAll().FirstOrDefaultAsync(CreateEqualityExpressionForId(id));
        }

        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetAll().FirstOrDefaultAsync(predicate);
        }

        public TEntity Insert(TEntity entity)
        {
            return Table.Add(entity).Entity;
        }

        public Task<TEntity> InsertAsync(TEntity entity)
        {
            return Task.FromResult(Insert(entity));
        }

        public int InsertAndGetId(TEntity entity)
        {
            entity = Insert(entity);
            _context.SaveChanges();
            return entity.Id;
        }

        public async Task<int> InsertAndGetIdAsync(TEntity entity)
        {
            entity = await InsertAsync(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public int InsertOrUpdateAndGetId(TEntity entity)
        {
            entity = InsertOrUpdate(entity);
            _context.SaveChanges();
            return entity.Id;
        }

        public async Task<int> InsertOrUpdateAndGetIdAsync(TEntity entity)
        {
            entity = await InsertOrUpdateAsync(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public TEntity Update(TEntity entity)
        {
            AttachIfNot(entity);
            _context.Entry(entity).State = EntityState.Modified;
            _context.Entry(entity).Property(x => x.Created).IsModified = false;
            return entity;
        }

        public Task<TEntity> UpdateAsync(TEntity entity)
        {
            entity = Update(entity);
            return Task.FromResult(entity);
        }

        public void Delete(TEntity entity)
        {
            AttachIfNot(entity);
            entity.Deleted = true;
        }

        public void Delete(int id)
        {
            var entity = GetFromChangeTrackerOrNull(id);
            if (entity != null)
            {
                Delete(entity);
                return;
            }

            entity = FirstOrDefault(id);
            if (entity != null)
            {
                Delete(entity);
            }

            //Could not found the entity, do nothing.
        }

        public async Task<int> CountAsync()
        {
            return await GetAll().CountAsync();
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetAll().Where(predicate).CountAsync();
        }

        public async Task<long> LongCountAsync()
        {
            return await GetAll().LongCountAsync();
        }

        public async Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetAll().Where(predicate).LongCountAsync();
        }

        private void AttachIfNot(TEntity entity)
        {
            var entry = _context.ChangeTracker.Entries().FirstOrDefault(ent => ent.Entity == entity);
            if (entry != null)
            {
                return;
            }

            Table.Attach(entity);
        }

        public DbContext GetDbContext()
        {
            return _context;
        }

        public Task EnsureCollectionLoadedAsync<TProperty>(
            TEntity entity,
            Expression<Func<TEntity, IEnumerable<TProperty>>> collectionExpression,
            CancellationToken cancellationToken)
            where TProperty : class
        {
            return _context.Entry(entity).Collection(collectionExpression).LoadAsync(cancellationToken);
        }

        public Task EnsurePropertyLoadedAsync<TProperty>(
            TEntity entity,
            Expression<Func<TEntity, TProperty>> propertyExpression,
            CancellationToken cancellationToken)
            where TProperty : class
        {
            return _context.Entry(entity).Reference(propertyExpression).LoadAsync(cancellationToken);
        }

        public TEntity InsertOrUpdate(TEntity entity)
        {
            return entity.Id <= 0 ? Insert(entity) : Update(entity);
        }

        public async Task<TEntity> InsertOrUpdateAsync(TEntity entity)
        {
            return entity.Id <= 0 ? await InsertAsync(entity) : await UpdateAsync(entity);
        }
        public TEntity FirstOrDefault(int id)
        {
            return GetAll().FirstOrDefault(CreateEqualityExpressionForId(id));
        }

        public Page<TEntity> Page(IQueryable<TEntity> source, PageParam pageParam)
        {
            var orderByName = pageParam.OrderBy;
            if (!orderByName.IsNullOrWhiteSpace()&&orderByName.ToUpper() == "RANDOM")
                source = source.OrderBy(r => Guid.NewGuid());
            else
            {
                if (pageParam.OrderBy.IsNullOrWhiteSpace() || typeof(TEntity).GetProperties().FirstOrDefault(p => p.Name == pageParam.OrderBy) == null)
                    orderByName = _context.Model.FindEntityType(typeof(TEntity)).FindPrimaryKey().Properties.Select(x => x.Name).Single();
                var orderProp = TypeDescriptor.GetProperties(typeof(TEntity)).Find(orderByName, true);
                switch (pageParam.OrderType)
                {
                    case OrderType.Asc:
                        source = source.OrderBy(s => orderProp.GetValue(s));
                        break;
                    default:
                        source = source.OrderByDescending(s => orderProp.GetValue(s));
                        break;
                }
            }
            var total = source.Count();
            var rows = source.Skip((pageParam.Page - 1) * pageParam.Limit).Take(pageParam.Limit).ToList();
            return new Page<TEntity>(total, pageParam.Page++, pageParam.Limit, rows);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        private TEntity GetFromChangeTrackerOrNull(int id)
        {
            var entry = _context.ChangeTracker.Entries()
                .FirstOrDefault(
                    ent =>
                        ent.Entity is TEntity &&
                        EqualityComparer<int>.Default.Equals(id, (ent.Entity as TEntity).Id)
                );

            return entry?.Entity as TEntity;
        }

        private Expression<Func<TEntity, bool>> CreateEqualityExpressionForId(int id)
        {
            var lambdaParam = Expression.Parameter(typeof(TEntity));

            var leftExpression = Expression.PropertyOrField(lambdaParam, "Id");

            Expression<Func<object>> closure = () => id;
            var rightExpression = Expression.Convert(closure.Body, leftExpression.Type);

            var lambdaBody = Expression.Equal(leftExpression, rightExpression);

            return Expression.Lambda<Func<TEntity, bool>>(lambdaBody, lambdaParam);
        }
    }
}
