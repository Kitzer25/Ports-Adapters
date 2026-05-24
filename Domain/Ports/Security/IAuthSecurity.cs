using Domain.Entities;

namespace Domain.Ports.Security;

public interface IAuthSecurity
{
    string GenerateToken(User? user);
}