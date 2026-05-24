using Domain.Entities;
using Domain.ValueObjects;

namespace Domain.Ports.Persistence.Repositories;

public interface IRoleRepository
{
    Task<Role?> GetByRolName(RoleName rolname, CancellationToken ct);
}