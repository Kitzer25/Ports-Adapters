using Domain.ValueObjects;

namespace Domain.Entities;

public class User
{
    private readonly List<Role> _roles = new();

    public Guid Id { get; private set; }
    public Username Username { get; private set; }
    public string PasswordHash { get; private set; }
    public Email Email { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public IReadOnlyCollection<Role> Roles => _roles;

    public User(Guid id, Username username, string passwordHash, Email email)
    {
        if (string.IsNullOrWhiteSpace(username.ToString()))
            throw new ArgumentException("Username requerido");

        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new ArgumentException("Password requerido");

        Id = id;
        Username = username;
        PasswordHash = passwordHash;
        Email = email;
        CreatedAt = DateTime.UtcNow;
    }

    public void AssignRole(Role? role)
    {
        if (_roles.Any(r => role != null && r.Id == role.Id))
            throw new InvalidOperationException("El usuario ya tiene este rol");

        if (role != null) _roles.Add(role);
    }
}