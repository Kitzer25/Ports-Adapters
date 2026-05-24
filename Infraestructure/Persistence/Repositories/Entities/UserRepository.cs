using Domain.Entities;
using Domain.Ports.Repositories;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Persistence.Repositories.Entities;

public class UserRepository : 
    GRepository<User>,
    IUserRepository
{
    public UserRepository(AppDbContext context) : base(context)
    { }


    public async Task<User?> GetByUsername(Username username, CancellationToken ct)
    {
        return await _dbSet.AsNoTracking().FirstOrDefaultAsync(u => u.Username == username, ct);
    }
}