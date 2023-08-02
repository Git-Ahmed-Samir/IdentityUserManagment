using System.Linq.Expressions;

namespace IdentityUserManagment.Domain.IRepositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity GetById(int id);
        TEntity GetById(long id);
        TEntity GetById(string id);
        IEnumerable<TEntity> GetAll();
        bool CheckIfExists(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> Where(Expression<Func<TEntity, bool>> predicate);
        TEntity Single(Expression<Func<TEntity, bool>> predicate);
        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);
        TEntity First(Expression<Func<TEntity, bool>> predicate);
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);
        TEntity CreateInstance();
        TEntity Add(TEntity entity);
        IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities);
        TEntity AddOrUpdate(TEntity entities);
        IEnumerable<TEntity> AddOrUpdateRange(IEnumerable<TEntity> entities);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);

        //Task Async
        /// <summary>
            /// Get query for entity
            /// </summary>
            /// <param name="filter"></param>
            /// <param name="orderBy"></param>
            /// <returns></returns>
        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null);
        /// <summary>
            /// Get all entities from db
            /// </summary>
            /// <param name="filter"></param>
            /// <param name="orderBy"></param>
            /// <param name="includes"></param>
            /// <returns></returns>
        Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, params Expression<Func<TEntity, object>>[] includes);
        Task<List<TEntity>> GetAsyncNoTracking(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, params Expression<Func<TEntity, object>>[] includes);
        /// <summary>
            /// Get single entity by primary key
            /// </summary>
            /// <param name="id"></param>
            /// <returns></returns>
        Task<TEntity> GetByIdAsync(object id);
        /// <summary>
            /// Get first or default entity by filter
            /// </summary>
            /// <param name="filter"></param>
            /// <param name="includes"></param>
            /// <returns></returns>
        Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> filter = null, params Expression<Func<TEntity, object>>[] includes);
        Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter = null, params Expression<Func<TEntity, object>>[] includes);
        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        Task<List<TEntity>> GetAllAsync();
        Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> predicate);
    }

}
