using Domain.Entities;

namespace Domain.ValueObjects;

public class RoleName
{
    public string Value { get; }

    public RoleName(string value)
    {
        Value = value;
    }

    public static RoleName User => new("User");
    public static RoleName Admin => new("Admin");
}