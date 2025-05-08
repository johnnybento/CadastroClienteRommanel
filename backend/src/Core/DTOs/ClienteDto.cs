
using CadastroClienteRommanel.Core.Enums;

namespace CadastroClienteRommanel.Core.DTOs;

public sealed class ClienteDto
{
    public Guid Id { get; init; }
    public string CpfCnpj { get; init; } = default!;
    public string Email { get; init; } = default!;
    public string Telefone { get; init; } = default!;
    public string Cep { get; init; } = default!;
    public string Logradouro { get; init; } = default!;
    public string Numero { get; init; } = default!;
    public string Bairro { get; init; } = default!;
    public string Cidade { get; init; } = default!;
    public string Estado { get; init; } = default!;
    public TipoCliente Tipo { get; init; }
    public string Ie { get; init; } = default!;
    public DateTime? DataNascimento { get; init; }
    public DateTime DataRegistro { get; init; }
}
