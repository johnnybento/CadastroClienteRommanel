
using CadastroClienteRommanel.Core.Enums;
using CadastroClienteRommanel.Core.Events;
using CadastroClienteRommanel.Core.Exceptions;
using CadastroClienteRommanel.Core.ValueObjects;

namespace CadastroClienteRommanel.Core.Entities;

public sealed class Cliente
{
    public Guid Id { get; private set; }

    public CpfCnpj CpfCnpj { get; private set; }
    public Email Email { get; private set; }
    public Telefone Telefone { get; private set; }
    public Endereco Endereco { get; private set; }

    public TipoCliente Tipo { get; private set; }
    public string? Ie { get; private set; }
    public DateTime? DataNascimento { get; private set; }
    public DateTime DataRegistro { get; private set; }

    private readonly List<object> _eventos = new();
    public IReadOnlyCollection<object> EventosDominio => _eventos.AsReadOnly();

    private Cliente() { } //

    public static Cliente Criar(
        CpfCnpj cpfCnpj,
        Email email,
        Telefone telefone,
        Endereco endereco,
        TipoCliente tipo,
        string? ie,
        DateTime? dataNascimento = null)
    {
        if (tipo == TipoCliente.PessoaFisica)
        {
            if (!dataNascimento.HasValue)
                throw new DomainException("Data de nascimento é obrigatória para pessoa física.");

            var idade = CalcularIdade(dataNascimento.Value);
            if (idade < 18)
                throw new DomainException("Menor de idade. Deve ter no mínimo 18 anos.");
        }
        if (tipo == TipoCliente.PessoaJuridica && string.IsNullOrWhiteSpace(ie))
            throw new DomainException("Para pessoa jurídica, informe IE ou 'isento'.");

        var cliente = new Cliente
        {
            Id = Guid.NewGuid(),
            CpfCnpj = cpfCnpj,
            Email = email,
            Telefone = telefone,
            Endereco = endereco,
            Tipo = tipo,
            Ie = ie,
            DataNascimento = dataNascimento,
            DataRegistro = DateTime.UtcNow
        };

        cliente.AdicionarEvento(new ClienteRegistrado(cliente.Id, cliente.DataRegistro));

        return cliente;
    }

    private static int CalcularIdade(DateTime nascimento)
    {
        var hoje = DateTime.UtcNow.Date;
        var idade = hoje.Year - nascimento.Year;
        if (nascimento.Date > hoje.AddYears(-idade)) idade--;
        return idade;
    }

    private void AdicionarEvento(object evento)
        => _eventos.Add(evento);
}
