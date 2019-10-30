using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iBlogs.Site.Core.Common.Request;
using iBlogs.Site.Core.Common.Response;

namespace iBlogs.Site.Core.Storage
{
    public interface IRepository<TEntity> where TEntity : class, IEntityBase
    {
        IEnumerable<TEntity> GetAll();
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
        int Count();
        TEntity InsertOrUpdate(TEntity entity);
        Task<TEntity> InsertOrUpdateAsync(TEntity entity);
        TEntity FirstOrDefault(int id);
        Page<TEntity> Page(IOrderedEnumerable<TEntity> source, PageParam pageParam);
    }
}