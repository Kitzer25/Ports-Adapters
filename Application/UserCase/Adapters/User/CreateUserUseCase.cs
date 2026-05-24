using Application.UserCase.Ports.User;
using Domain.Ports;
using Domain.Ports.Repositories;
using Domain.Ports.Security;
using Domain.ValueObjects;
using Microsoft.Extensions.Configuration;

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
        
        if (userExist  != null || emailExist != null )
            throw new Exception("Username or Email already exists");
        
        Console.WriteLine("PRUEBA 1 COMPLETADA");
        
        var defaultRole = await _unitOfWork.RoleRepository.GetByRolName("User", ct);
        
        Console.WriteLine($"dATOS DE ROLE: {defaultRole.ToString()}");
        
        if  (defaultRole == null) throw new Exception("Default role not found");
        
        var passwordHash = _passwordHasher.Hash(password);
        
        var user = new Domain.Entities.User(
            Guid.NewGuid(),
            username,
            passwordHash,
            email
        );
        
        Console.WriteLine("Registro de usuario pasó la prueba", user);
        
        user.AssignRole(defaultRole.Id);

        await _unitOfWork.UserRepository.Add(user, ct);
    }
}