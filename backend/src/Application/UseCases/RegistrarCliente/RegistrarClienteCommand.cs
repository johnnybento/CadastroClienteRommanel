using System;
using System.Text.Json.Serialization;
using CadastroClienteRommanel.Core.Enums;
using MediatR;

namespace CadastroClienteRommanel.Application.UseCases.RegistrarCliente;

public sealed record RegistrarClienteCommand : IRequest<Guid>
{
    [JsonConstructor]
    public RegistrarClienteCommand(
        string cpfCnpj,
        string email,
        string telefone,
        string cep,
        string logradouro,
        string numero,
        string bairro,
        string cidade,
        string estado,
        TipoCliente tipo,
        string? ie,
        DateTime? dataNascimento
    )
    {
        CpfCnpj = cpfCnpj;
        Email = email;
        Telefone = telefone;
        Cep = cep;
        Logradouro = logradouro;
        Numero = numero;
        Bairro = bairro;
        Cidade = cidade;
        Estado = estado;
        Tipo = tipo;
        Ie = ie;
        DataNascimento = dataNascimento;
    }

    public string CpfCnpj { get; init; }
    public string Email { get; init; }
    public string Telefone { get; init; }
    public string Cep { get; init; }
    public string Logradouro { get; init; }
    public string Numero { get; init; }
    public string Bairro { get; init; }
    public string Cidade { get; init; }
    public string Estado { get; init; }
    public TipoCliente Tipo { get; init; }
    public string? Ie { get; init; }
    public DateTime? DataNascimento { get; init; }
}
