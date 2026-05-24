using System.Text.RegularExpressions;
using Domain.Errors;

namespace Domain.ValueObjects;

public sealed class Email : IEquatable<Email>
{
    public string Value { get; }
    
    private static readonly Regex EmailRegex = 
        new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);

    public Email(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainException("El email es requerido");

        if (!EmailRegex.IsMatch(value))
            throw new DomainException("Email inválido");

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