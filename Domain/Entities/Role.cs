namespace Domain.Entities;

public class Role
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    
    private Role() { }

    public Role(Guid id, string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("El nombre del rol es obligatorio");

        Id = id;
        Name = name;
    }
}