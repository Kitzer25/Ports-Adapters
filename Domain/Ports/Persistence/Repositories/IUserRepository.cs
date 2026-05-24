using Domain.Entities;
using Domain.ValueObjects;

namespace Domain.Ports.Persistence.Repositories;

public interface IUserRepository : IGRepository<User>
{
    Task<User?> GetByUsername(Username username, CancellationToken ct);
    Task<User?> GetByEmail(Email email, CancellationToken ct);
}