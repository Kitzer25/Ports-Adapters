using Domain.Entities;
using Domain.Ports;
using Domain.Ports.Repositories;
using Infraestructure.Persistence.Repositories.Entities;

namespace Infraestructure.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private readonly IDictionary<Type, object> _repositories;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        _repositories = new Dictionary<Type, object>();

        UserRepository = new UserRepository(_context);
        RoleRepository = new RoleRepository(_context);
    }
    
    //Repositorios
    public IUserRepository UserRepository { get; }
    public IRoleRepository RoleRepository { get; }


    public IGRepository<T> Repositories<T>() where T : class
    {
        var type = typeof(T);

        if (_repositories.TryGetValue(type, out var repositories))
        {
            return (IGRepository<T>)repositories;
        }

        var repositoryInstance = new GRepository<T>(_context);
        _repositories.Add(type, repositoryInstance);
        
        return repositoryInstance;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}