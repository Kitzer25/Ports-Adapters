using Application.DTO_s.AuthDTO;
using Domain.ValueObjects;

namespace Application.UserCase.Ports.User;

public interface ILoginUserUseCase
{
    Task<LoginResponseDto> Execute(Username username, string password, CancellationToken ct);
}