using Domain.Ports.Repositories;
using Domain.Ports.Security;
using Domain.ValueObjects;

namespace Application.UserCase.User;

public class CreateUserUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IRoleRepository _roleRepository;

    public CreateUserUseCase(
        IUserRepository userRepository, 
        IPasswordHasher passwordHasher,
        IRoleRepository roleRepository)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _roleRepository = roleRepository;
    }

    public async Task Execute(
        Username username,
        Email email,
        string password,
        CancellationToken ct)
    {
        var defaultRole = await _roleRepository.GetByRolName("User", ct);
        var passwordHash = _passwordHasher.Hash(password);
        
        var user = new Domain.Entities.User(
            Guid.NewGuid(),
            new Username(username.Value),
            passwordHash,
            new Email(email.Value)
        );
        
        user.AssignRole(defaultRole);

        await _userRepository.Add(user, ct);
    }
}