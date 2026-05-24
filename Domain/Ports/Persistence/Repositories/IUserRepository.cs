using Domain.Entities;
using Domain.ValueObjects;

namespace Domain.Ports.Repositories;

public interface IUserRepository : IGRepository<User>
{
    Task<User?> GetByUsername(Username username, CancellationToken ct);
}