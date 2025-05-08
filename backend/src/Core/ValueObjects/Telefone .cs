using CadastroClienteRommanel.Core.Exceptions;
using System.Text.RegularExpressions;

namespace CadastroClienteRommanel.Core.ValueObjects;

public sealed class Telefone : IEquatable<Telefone>
{
    public string Numero { get; }

    private Telefone(string numero)
    {
        Numero = numero;
    }
    public Telefone() { }
    public static Telefone Criar(string telefone)
    {
        if (string.IsNullOrWhiteSpace(telefone))
            throw new DomainException("Telefone não pode ser vazio.");

        var apenasDigitos = Regex.Replace(telefone, @"[^\d\+]", "");

        var padrao = @"^\+?\d{8,15}$";
        if (!Regex.IsMatch(apenasDigitos, padrao))
            throw new DomainException("Formato de telefone inválido.");

        return new Telefone(apenasDigitos);
    }

    public override bool Equals(object? obj) => Equals(obj as Telefone);
    public bool Equals(Telefone? other) => other is not null && other.Numero == Numero;
    public override int GetHashCode() => HashCode.Combine(Numero);
    public override string ToString() => Numero;

    public static implicit operator string(Telefone tel) => tel.Numero;

}
