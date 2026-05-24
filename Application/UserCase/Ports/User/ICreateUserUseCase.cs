using Domain.ValueObjects;

namespace Application.UserCase.Ports.User;

public interface ICreateUserUseCase
{
    Task Execute(Username username, Email email, string password, CancellationToken ct);
}