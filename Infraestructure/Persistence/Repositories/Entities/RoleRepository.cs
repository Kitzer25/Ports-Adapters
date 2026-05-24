using Domain.Entities;
using Domain.Ports.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Persistence.Repositories.Entities;

public class RoleRepository : 
    GRepository<Role>,
    IRoleRepository
{
    public RoleRepository(AppDbContext context) : base(context)
    { }

    public async Task<Role?> GetByRolName(string rolname, CancellationToken ct)
    {
        return await _dbSet.AsNoTracking()
            .FirstOrDefaultAsync(r => r.Name == rolname);
    }
}