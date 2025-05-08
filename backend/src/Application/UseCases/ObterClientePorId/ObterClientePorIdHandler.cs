
using CadastroClienteRommanel.Core.DTOs;
using CadastroClienteRommanel.Core.Interfaces;
using MediatR;

namespace CadastroClienteRommanel.Application.UseCases.ObterClientePorId;

public class ObterClientePorIdHandler : IRequestHandler<ObterClientePorIdQuery, ClienteDto?>
{
    private readonly IClienteRepository _repositorio;

    public ObterClientePorIdHandler(IClienteRepository repositorio)
        => _repositorio = repositorio;

    public async Task<ClienteDto?> Handle(ObterClientePorIdQuery request, CancellationToken cancellationToken)
    {
        var cliente = await _repositorio.ObterPorIdAsync(request.Id);
        if (cliente is null) return null;

        // Mapeia manualmente para DTO
        return new ClienteDto
        {
            Id = cliente.Id,
            CpfCnpj = cliente.CpfCnpj.Value,
            Email = cliente.Email.Value,
            Telefone = cliente.Telefone.Numero,
            Cep = cliente.Endereco.CEP,
            Logradouro = cliente.Endereco.Logradouro,
            Numero = cliente.Endereco.Numero,
            Bairro = cliente.Endereco.Bairro,
            Cidade = cliente.Endereco.Cidade,
            Estado = cliente.Endereco.Estado,
            Tipo = cliente.Tipo,
            Ie = cliente.Ie,
            DataNascimento = cliente.DataNascimento,
            DataRegistro = cliente.DataRegistro
        };
    }
}