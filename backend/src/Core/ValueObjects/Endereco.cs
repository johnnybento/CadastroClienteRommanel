using CadastroClienteRommanel.Core.Exceptions;
using System.Text.RegularExpressions;

namespace CadastroClienteRommanel.Core.ValueObjects;

public sealed class Endereco : IEquatable<Endereco>
{

    public string CEP { get; }
    public string Logradouro { get; }
    public string Numero { get; }
    public string Bairro { get; }
    public string Cidade { get; }
    public string Estado { get; }
    public Endereco() { }
    private Endereco(
        string cep,
        string logradouro,
        string numero,
        string bairro,
        string cidade,
        string estado)
    {
        CEP = cep;
        Logradouro = logradouro;
        Numero = numero;
        Bairro = bairro;
        Cidade = cidade;
        Estado = estado;
    }

    public static Endereco Criar(
        string cep,
        string logradouro,
        string numero,
        string bairro,
        string cidade,
        string estado)
    {
        if (string.IsNullOrWhiteSpace(cep)
         || string.IsNullOrWhiteSpace(logradouro)
         || string.IsNullOrWhiteSpace(numero)
         || string.IsNullOrWhiteSpace(bairro)
         || string.IsNullOrWhiteSpace(cidade)
         || string.IsNullOrWhiteSpace(estado))
        {
            throw new DomainException("Todos os campos de endereço são obrigatórios.");
        }
        var cepLimpo = cep.Trim();
        var padraoCep = @"^\d{5}-?\d{3}$";
        if (!Regex.IsMatch(cepLimpo, padraoCep))
            throw new DomainException("CEP inválido. Formato esperado: 00000-000 ou 00000000.");
        var sigla = estado.Trim().ToUpperInvariant();
        if (!Regex.IsMatch(sigla, @"^[A-Z]{2}$"))
            throw new DomainException("Estado inválido. Use a sigla de duas letras, ex: SP, RJ.");

        return new Endereco(
            cepLimpo,
            logradouro.Trim(),
            numero.Trim(),
            bairro.Trim(),
            cidade.Trim(),
            sigla);
    }

    public override bool Equals(object? obj) => Equals(obj as Endereco);
    public bool Equals(Endereco? other) =>
        other is not null
     && other.CEP == CEP
     && other.Logradouro == Logradouro
     && other.Numero == Numero
     && other.Bairro == Bairro
     && other.Cidade == Cidade
     && other.Estado == Estado;

    public override int GetHashCode() =>
        HashCode.Combine(CEP, Logradouro, Numero, Bairro, Cidade, Estado);

    public override string ToString() =>
        $"{Logradouro}, {Numero} - {Bairro}, {Cidade}/{Estado}, CEP {CEP}";

}
