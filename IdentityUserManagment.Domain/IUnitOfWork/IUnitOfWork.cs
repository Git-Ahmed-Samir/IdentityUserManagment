using IdentityUserManagment.Domain.IRepositories;

namespace IdentityUserManagment.Domain.IUnitOfWork
{
    public interface IUnitOfWork
    {
        IRepository<T> Repository<T>() where T : class;

        bool Complete();
        Task<bool> CompleteAsync();
        bool HasChanges();

    }
}
