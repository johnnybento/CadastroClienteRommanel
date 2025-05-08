
using CadastroClienteRommanel.Core.Entities;
using CadastroClienteRommanel.Core.Exceptions;
using CadastroClienteRommanel.Core.Interfaces;
using CadastroClienteRommanel.Core.ValueObjects;
using MediatR;

namespace CadastroClienteRommanel.Application.UseCases.RegistrarCliente;

public class RegistrarClienteHandler : IRequestHandler<RegistrarClienteCommand, Guid>
{
    private readonly IClienteRepository _repositorio;

    public RegistrarClienteHandler(IClienteRepository repositorio)
        => _repositorio = repositorio;

    public async Task<Guid> Handle(RegistrarClienteCommand cmd, CancellationToken cancellationToken)
    {

        var cpfCnpj = CpfCnpj.Criar(cmd.CpfCnpj);
        var email = Email.Criar(cmd.Email);
        var telefone = Telefone.Criar(cmd.Telefone);
        var endereco = Endereco.Criar(
                            cmd.Cep, cmd.Logradouro,
                            cmd.Numero, cmd.Bairro,
                            cmd.Cidade, cmd.Estado);
       
        if (await _repositorio.ExistePorCpfCnpjAsync(cpfCnpj))
            throw new DomainException("CPF/CNPJ já cadastrado.");
        if (await _repositorio.ExistePorEmailAsync(email))
            throw new DomainException("E-mail já cadastrado.");

        var cliente = Cliente.Criar(
            cpfCnpj, email, telefone, endereco,
            cmd.Tipo, cmd.Ie, cmd.DataNascimento);

        await _repositorio.AdicionarAsync(cliente);
        return cliente.Id;
    }
}