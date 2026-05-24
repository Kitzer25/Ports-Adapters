using Domain.Entities;
using Domain.Ports.Persistence.Repositories;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Persistence.Repositories.Entities;

public class RoleRepository : 
    GRepository<Role>,
    IRoleRepository
{
    public RoleRepository(AppDbContext context) : base(context)
    { }

    public async Task<Role?> GetByRolName(RoleName rolname, CancellationToken ct)
    {
        return await _dbSet.AsNoTracking()
            .FirstOrDefaultAsync(r => r.Name == rolname);
    }
}