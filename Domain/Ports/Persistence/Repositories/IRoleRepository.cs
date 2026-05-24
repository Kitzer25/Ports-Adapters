using Domain.Entities;

namespace Domain.Ports.Repositories;

public interface IRoleRepository
{
    Task<Role?> GetByRolName(string rolname, CancellationToken ct);
}