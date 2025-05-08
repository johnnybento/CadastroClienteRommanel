using CadastroClienteRommanel.Core.Entities;
using CadastroClienteRommanel.Core.Enums;
using CadastroClienteRommanel.Core.Exceptions;
using CadastroClienteRommanel.Core.Tests.Tests.Entities.TestData;
using CadastroClienteRommanel.Core.ValueObjects;
using FluentAssertions;

namespace CadastroClienteRommanel.Core.Tests.Tests.Entities;

public class ClienteTests
{
    [Fact]
    public void Criar_PessoaFisica_MenorDe18_DeveLancarDomainException()
    {
        // Arrange
        var cpf = CpfCnpj.Criar("11144477735");
        var email = Email.Criar("teste@exemplo.com");
        var telefone = Telefone.Criar("+5511999999999");
        var endereco = Endereco.Criar("12345-678", "Rua A", "100", "Bairro B", "Cidade C", "SP");
        var dataMenor = DateTime.UtcNow.AddYears(-17);

        // Act
        Action act = () => Cliente.Criar(
            cpf, email, telefone, endereco,
            TipoCliente.PessoaFisica,
            ie: "isento",
            dataNascimento: dataMenor
        );

        // Assert
        act.Should()
           .Throw<DomainException>()
           .WithMessage("*18 anos*");
    }

    [Fact]
    public void Criar_PessoaFisica_Valido_DeveCriarCliente()
    {
        // Arrange
        var cliente = ClienteTestData.ClienteFisicoValido();

        // Act
        // (o factory já foi executado no TestData)

        // Assert
        cliente.Should().NotBeNull();
        cliente.Id.Should().NotBeEmpty();
        cliente.Tipo.Should().Be(TipoCliente.PessoaFisica);
        cliente.DataNascimento.Should().BeBefore(DateTime.UtcNow.AddYears(-17));
        cliente.EventosDominio.Should().ContainSingle(e => e.GetType().Name == nameof(Core.Events.ClienteRegistrado));
    }

    [Fact]
    public void Criar_PessoaJuridica_SemIe_DeveLancarDomainException()
    {
        // Arrange
        var cpfCnpj = CpfCnpj.Criar("11222333000181"); // CNPJ válido
        var email = Email.Criar("pj@exemplo.com");
        var telefone = Telefone.Criar("+5511988888888");
        var endereco = Endereco.Criar("87654-321", "Av. X", "200", "Bairro Y", "Cidade Z", "RJ");

        // Act
        Action act = () => Cliente.Criar(
            cpfCnpj, email, telefone, endereco,
            TipoCliente.PessoaJuridica,
            ie: "",                
            dataNascimento: null   
        );

        // Assert
        act.Should()
           .Throw<DomainException>()
           .WithMessage("*IE*");
    }

    [Fact]
    public void Criar_PessoaJuridica_Valido_DeveCriarCliente()
    {
        // Arrange
        var cliente = ClienteTestData.ClienteJuridicoValido();

        // Act
        // (factory já disparado no TestData)

        // Assert
        cliente.Should().NotBeNull();
        cliente.Id.Should().NotBeEmpty();
        cliente.Tipo.Should().Be(TipoCliente.PessoaJuridica);
        cliente.Ie.Should().NotBeNullOrWhiteSpace();
        cliente.EventosDominio.Should().ContainSingle(e => e.GetType().Name == nameof(Core.Events.ClienteRegistrado));
    }
}