using CadastroClienteRommanel.Application.Tests.Tests.UsesCases.RegistrarCliente.TestData;
using CadastroClienteRommanel.Application.UseCases.RegistrarCliente;
using CadastroClienteRommanel.Core.Entities;
using CadastroClienteRommanel.Core.Exceptions;
using CadastroClienteRommanel.Core.Interfaces;
using CadastroClienteRommanel.Core.ValueObjects;
using FluentAssertions;
using Moq;


namespace CadastroClienteRommanel.Application.Tests.Tests.UsesCases.RegistrarCliente;

public class RegistrarClienteHandlerTests
{
    private readonly Mock<IClienteRepository> _repo;
    private readonly RegistrarClienteHandler _handler;

    public RegistrarClienteHandlerTests()
    {
        _repo = new Mock<IClienteRepository>();
        _handler = new RegistrarClienteHandler(_repo.Object);
    }

    [Fact]
    public async Task Handle_ComandoValido_DeveRetornarNovoId()
    {
        // Arrange
        var cmd = RegistrarClienteTestData.ComandoValidoPF();
        _repo.Setup(r => r.ExistePorCpfCnpjAsync(It.IsAny<CpfCnpj>())).ReturnsAsync(false);
        _repo.Setup(r => r.ExistePorEmailAsync(It.IsAny<Email>())).ReturnsAsync(false);

        // Act
        var result = await _handler.Handle(cmd, CancellationToken.None);

        // Assert
        result.Should().NotBe(Guid.Empty);
        _repo.Verify(r => r.AdicionarAsync(It.IsAny<Cliente>()), Times.Once);
    }

    [Fact]
    public async Task Handle_CpfDuplicado_DeveLancarDomainException()
    {
        // Arrange
        var cmd = RegistrarClienteTestData.ComandoValidoPF();
        _repo.Setup(r => r.ExistePorCpfCnpjAsync(It.IsAny<CpfCnpj>())).ReturnsAsync(true);

        // Act
        Func<Task> act = async () => await _handler.Handle(cmd, CancellationToken.None);

        // Assert
        await act.Should()
                 .ThrowAsync<DomainException>()
                 .WithMessage("CPF/CNPJ já cadastrado.");
    }
}