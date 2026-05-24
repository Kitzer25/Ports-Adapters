using Domain.Errors;

namespace Domain.ValueObjects;

public sealed class Username : IEquatable<Username>
{
    public string Value { get; }

    public Username(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainException("Username requerido");

        if (value.Length < 3)
            throw new DomainException("Debe tener al menos 3 caracteres");

        Value = value;
    }

    public override string ToString() => Value;

    public bool Equals(Username? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Value == other.Value;
    }

    public override bool Equals(object? obj)
    {
        return obj is Username other && Equals(other);
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}