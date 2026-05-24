namespace Domain.ValueObjects;

public sealed class Username
{
    public string Value { get; }

    public Username(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Username requerido");

        if (value.Length < 3)
            throw new ArgumentException("Debe tener al menos 3 caracteres");

        Value = value;
    }

    public override string ToString() => Value;
}