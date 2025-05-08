
using CadastroClienteRommanel.Core.Exceptions;
using System.Text.RegularExpressions;

namespace CadastroClienteRommanel.Core.ValueObjects;

public sealed class Email : IEquatable<Email>
{
    public string Value { get; }

    private Email(string value)
    {
        Value = value;
    }
    public Email() { }
    public static Email Criar(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new DomainException("E-mail não pode ser vazio.");

        var trimmed = email.Trim().ToLowerInvariant();
        var pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        if (!Regex.IsMatch(trimmed, pattern))
            throw new DomainException("Formato de e-mail inválido.");

        return new Email(trimmed);
    }

    public override bool Equals(object? obj) => Equals(obj as Email);
    public bool Equals(Email? other) => other is not null && other.Value == Value;
    public override int GetHashCode() => HashCode.Combine(Value);
    public override string ToString() => Value;

    public static implicit operator string(Email email) => email.Value;
}

