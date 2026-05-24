using Domain.Entities;
using Domain.Ports.Repositories;

namespace Domain.Ports;

public interface IUnitOfWork : IDisposable
{
    public IUserRepository UserRepository { get; }
    public IRoleRepository RoleRepository { get; }

    public IGRepository<T> Repositories<T>()  where T : class;
    
    Task<int> SaveChangesAsync();
}