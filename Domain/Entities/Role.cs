using Domain.ValueObjects;

namespace Domain.Entities;

public class Role
{
    public Guid Id { get; private set; }
    public RoleName Name { get; private set; }
    
    private Role() { }

    public Role(Guid id, RoleName name)
    {
        if (string.IsNullOrWhiteSpace(name.Value))
            throw new ArgumentException("El nombre del rol es obligatorio");

        Id = id;
        Name = name;
    }
}