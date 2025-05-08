using CadastroClienteRommanel.Core.Exceptions;
using System.Text.RegularExpressions;

namespace CadastroClienteRommanel.Core.ValueObjects;

public sealed class CpfCnpj
{
    public string Value { get; }
    public bool IsCpf { get; }

    private CpfCnpj(string valor, bool isCpf)
    {
        Value = valor;
        IsCpf = isCpf;
    }
   
    private CpfCnpj() { }
    public static CpfCnpj Criar(string valor)
    {
        if (string.IsNullOrWhiteSpace(valor))
            throw new DomainException("CPF/CNPJ não pode ser vazio.");

        // Apenas dígitos
        var digitos = Regex.Replace(valor, @"\D", "");

        if (digitos.Length == 11)
        {
            if (!ValidarCpf(digitos))
                throw new DomainException("CPF/CNPJ inválido.");
            return new CpfCnpj(digitos, true);
        }
        else if (digitos.Length == 14)
        {
            if (!ValidarCnpj(digitos))
                throw new DomainException("CPF/CNPJ inválido.");
            return new CpfCnpj(digitos, false);
        }
        else
        {
            throw new DomainException("Quantidade de dígitos inválida para CPF ou CNPJ.");
        }
    }

    private static bool ValidarCpf(string cpf)
    {
        if (cpf.Distinct().Count() == 1) return false;

        int[] mult1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] mult2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        string temp = cpf.Substring(0, 9);
        int soma = temp.Select((t, i) => (t - '0') * mult1[i]).Sum();
        int resto = soma % 11;
        resto = resto < 2 ? 0 : 11 - resto;
        string digito = resto.ToString();

        temp += digito;
        soma = temp.Select((t, i) => (t - '0') * mult2[i]).Sum();
        resto = soma % 11;
        resto = resto < 2 ? 0 : 11 - resto;
        digito += resto;

        return cpf.EndsWith(digito);
    }

    private static bool ValidarCnpj(string cnpj)
    {
        if (cnpj.Distinct().Count() == 1) return false;

        int[] mult1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] mult2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

        string temp = cnpj.Substring(0, 12);
        int soma = temp.Select((t, i) => (t - '0') * mult1[i]).Sum();
        int resto = soma % 11;
        resto = resto < 2 ? 0 : 11 - resto;
        string digito = resto.ToString();

        temp += digito;
        soma = temp.Select((t, i) => (t - '0') * mult2[i]).Sum();
        resto = soma % 11;
        resto = resto < 2 ? 0 : 11 - resto;
        digito += resto;

        return cnpj.EndsWith(digito);
    }

    public override bool Equals(object? obj) => Equals(obj as CpfCnpj);
    public bool Equals(CpfCnpj? other) =>
        other is not null &&
        other.Value == Value &&
        other.IsCpf == IsCpf;

    public override int GetHashCode() => HashCode.Combine(Value, IsCpf);
    public override string ToString() => Value;

    public static implicit operator string(CpfCnpj c) => c.Value;
}
