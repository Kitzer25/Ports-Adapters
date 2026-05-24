namespace Domain.ValueObjects;

public sealed class Email : IEquatable<Email>
{
    public string Value { get; }

    public Email(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("El email es requerido");

        if (!value.Contains("@"))
            throw new ArgumentException("Email inválido");

        Value = value;
    }

    public override bool Equals(object? obj)
        => Equals(obj as Email);

    public bool Equals(Email? other)
        => other != null && Value == other.Value;

    public override int GetHashCode()
        => Value.GetHashCode();

    public override string ToString() => Value;
}