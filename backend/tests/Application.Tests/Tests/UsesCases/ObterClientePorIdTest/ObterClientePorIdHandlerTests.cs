using CadastroClienteRommanel.Application.Tests.Tests.UsesCases.ObterClientePorIdTest.TestData;
using CadastroClienteRommanel.Application.UseCases.ObterClientePorId;
using CadastroClienteRommanel.Core.Entities;
using CadastroClienteRommanel.Core.Interfaces;
using FluentAssertions;
using Moq;

namespace CadastroClienteRommanel.Application.Tests.Tests.UsesCases.ObterClientePorIdTest;

public class ObterClientePorIdHandlerTests
{
    private readonly Mock<IClienteRepository> _repo;
    private readonly ObterClientePorIdHandler _handler;

    public ObterClientePorIdHandlerTests()
    {
        _repo = new Mock<IClienteRepository>();
        _handler = new ObterClientePorIdHandler(_repo.Object);
    }

    [Fact]
    public async Task Handle_ClienteExistente_DeveRetornarDto()
    {
        // Arrange
        var cliente = ObterClientePorIdTestData.ClienteFisicoValido();
        _repo.Setup(r => r.ObterPorIdAsync(cliente.Id))
             .ReturnsAsync(cliente);

        var query = new ObterClientePorIdQuery(cliente.Id);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result!.Id.Should().Be(cliente.Id);
        result.CpfCnpj.Should().Be(cliente.CpfCnpj.Value);
        result.Email.Should().Be(cliente.Email.Value);
        result.Telefone.Should().Be(cliente.Telefone.Numero);
        result.Cidade.Should().Be(cliente.Endereco.Cidade);
    }

    [Fact]
    public async Task Handle_ClienteNaoExiste_DeveRetornarNull()
    {
        // Arrange
        _repo.Setup(r => r.ObterPorIdAsync(It.IsAny<Guid>()))
             .ReturnsAsync((Cliente?)null);

        var query = new ObterClientePorIdQuery(Guid.NewGuid());

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().BeNull();
    }
}