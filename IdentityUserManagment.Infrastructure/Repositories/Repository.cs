using IdentityUserManagment.Domain.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace IdentityUserManagment.Infrastructure.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        public readonly DbContext _context;
        protected readonly DbSet<TEntity> _entities;
        public Repository(DbContext context)
        {
            _context = context;
            _entities = context.Set<TEntity>();
        }
        public TEntity GetById(int id)
        {
            return _entities.Find(id);
        }
        public TEntity GetById(long id)
        {
            return _entities.Find(id);
        }
        public TEntity GetById(string id)
        {
            return _entities.Find(id);
        }
        public IEnumerable<TEntity> GetAll()
        {
            return _entities.ToList();
        }
        public bool CheckIfExists(Expression<Func<TEntity, bool>> predicate)
        {
            return _entities.Any(predicate);
        }

        public IEnumerable<TEntity> Where(Expression<Func<TEntity, bool>> predicate)
        {
            return _entities.Where(predicate);
        }
        public TEntity Single(Expression<Func<TEntity, bool>> predicate)
        {
            return _entities.Single(predicate);
        }
        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return _entities.SingleOrDefault(predicate);
        }
        public TEntity First(Expression<Func<TEntity, bool>> predicate)
        {
            return _entities.First(predicate);
        }
        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return _entities.FirstOrDefault(predicate);
        }
        public TEntity CreateInstance()
        {
            TEntity newTable = Activator.CreateInstance<TEntity>();
            return newTable;
        }
        public TEntity Add(TEntity entity)
        {
            _entities.Add(entity);
            return entity;
        }
        public IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities)
        {
            _entities.AddRange(entities);
            return entities;
        }
        public TEntity AddOrUpdate(TEntity entity)
        {
            _entities.Update(entity);
            return entity;
        }

        public IEnumerable<TEntity> AddOrUpdateRange(IEnumerable<TEntity> entities)
        {
            _entities.UpdateRange(entities);
            return entities;
        }

        public void Remove(TEntity entity)
        {
            _entities.Remove(entity);
        }
        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _entities.RemoveRange(entities);
        }

        //Task Async
        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            IQueryable<TEntity> query = _entities;

            if (filter != null)
                query = query.Where(filter);

            if (orderBy != null)
                query = orderBy(query);

            return query;
        }
        public Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _entities;

            foreach (Expression<Func<TEntity, object>> include in includes)
                query = query.Include(include);

            if (filter != null)
                query = query.Where(filter);

            if (orderBy != null)
                query = orderBy(query);

            return query.ToListAsync();
        }
        public Task<List<TEntity>> GetAsyncNoTracking(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _entities;

            foreach (Expression<Func<TEntity, object>> include in includes)
                query = query.Include(include);

            if (filter != null)
                query = query.Where(filter);

            if (orderBy != null)
                query = orderBy(query);

            return query.AsNoTracking().ToListAsync();
        }
        public async Task<TEntity> GetByIdAsync(object id)
        {
            return await _entities.FindAsync(id);
        }
        public Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> filter = null, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _entities;

            foreach (Expression<Func<TEntity, object>> include in includes)
                query = query.Include(include);

            return query.FirstAsync(filter);
        }
        public Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return _entities.SingleAsync(predicate);
        }
        public Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter = null, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _entities;

            foreach (Expression<Func<TEntity, object>> include in includes)
                query = query.Include(include);

            return query.FirstOrDefaultAsync(filter);
        }
        public Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return _entities.SingleOrDefaultAsync(predicate);
        }
        public Task<List<TEntity>> GetAllAsync()
        {
            return _entities.ToListAsync();
        }
        public Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return _entities.AnyAsync(predicate);
        }
    }


}