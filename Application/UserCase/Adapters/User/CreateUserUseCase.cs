using Application.UserCase.Ports.User;
using Domain.Errors;
using Domain.Ports;
using Domain.Ports.Security;
using Domain.ValueObjects;

namespace Application.UserCase.Adapters.User;

public class CreateUserUseCase : ICreateUserUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    
    private readonly IPasswordHasher _passwordHasher;

    public CreateUserUseCase(
        IUnitOfWork unitOfWork,
        IPasswordHasher passwordHasher)
    {
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
    }

    public async Task Execute(
        Username username,
        Email email,
        string password,
        CancellationToken ct)
    {
        var userExist = await _unitOfWork.UserRepository.GetByUsername(username, ct);
        var emailExist = await _unitOfWork.UserRepository.GetByEmail(email, ct);
        
        if (userExist != null)
            throw new DomainException("Username already exists");

        if (emailExist != null)
            throw new DomainException("Email already exists");

        var defaultRole = await _unitOfWork.RoleRepository.GetByRolName(RoleName.User, ct);
        
        if  (defaultRole == null) 
            throw new DomainException("Rol no encontrado");
        
        var passwordHash = _passwordHasher.Hash(password);
        
        var user = new Domain.Entities.User(
            Guid.NewGuid(),
            username,
            passwordHash,
            email
        );
        
        user.AssignRole(defaultRole.Id);

        await _unitOfWork.UserRepository.Add(user, ct);
    }
}