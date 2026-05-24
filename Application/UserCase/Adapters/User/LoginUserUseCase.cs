using System.Security.Authentication;
using Application.DTO_s.AuthDTO;
using Application.UserCase.Ports.User;
using Domain.Errors;
using Domain.Ports;
using Domain.Ports.Security;
using Domain.ValueObjects;

namespace Application.UserCase.Adapters.User;

public class LoginUserUseCase : ILoginUserUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IAuthSecurity _authSecurity;

    public LoginUserUseCase(
        IUnitOfWork unitOfWork,
        IPasswordHasher passwordHasher,
        IAuthSecurity authSecurity)
    {
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
        _authSecurity = authSecurity;
    }

    public async Task<LoginResponseDto> Execute(
        Username username,
        string password,
        CancellationToken ct)
    {
        var user = await _unitOfWork.UserRepository.GetByUsername(username, ct);

        if (user is null)
            throw new DomainException("Usuario no encontrado");

        var isValid = _passwordHasher.Verify(user.PasswordHash, password);

        if (!isValid)
            throw new InvalidCredentialException("Credenciales inválidas");

        var token = _authSecurity.GenerateToken(user);

        return new LoginResponseDto
        {
            Username = user.Username.Value,
            Token = token
        };
    }
}