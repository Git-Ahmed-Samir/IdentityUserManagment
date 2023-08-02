using IdentityUserManagment.Domain.IRepositories;
using IdentityUserManagment.Domain.IUnitOfWork;
using IdentityUserManagment.Infrastructure.Contexts;
using IdentityUserManagment.Infrastructure.Repositories;

namespace IdentityUserManagment.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private ApplicationDbContext _context;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public Dictionary<string, object> repositories;
        public IRepository<T> Repository<T>() where T : class
        {
            if (repositories == null)
                repositories = new Dictionary<string, object>();

            string type = typeof(T).Name;
            if (!repositories.ContainsKey(type))
            {
                var repositoryInstance = Activator.CreateInstance(typeof(Repository<>).MakeGenericType(typeof(T)), _context);
                repositories.Add(type, repositoryInstance);
            }
            return (Repository<T>)repositories[type];
        }

        public bool Complete()
        {
            //if (!CheckRequestExecutingByCurrentUser(currentUserId)) return false;

            return _context.SaveChanges() > 0 ? true : false;
        }

        public async Task<bool> CompleteAsync()
        {
            //if (!CheckRequestExecutingByCurrentUser(currentUserId)) return false;

            return await _context.SaveChangesAsync() > 0 ? true : false;
        }

        public bool HasChanges()
        {
            _context.ChangeTracker.DetectChanges();
            var changes = _context.ChangeTracker.HasChanges();

            return changes;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

    }
}
