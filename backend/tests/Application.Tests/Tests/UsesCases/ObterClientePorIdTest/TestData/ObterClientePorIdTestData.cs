using Bogus;
using CadastroClienteRommanel.Core.Entities;
using CadastroClienteRommanel.Core.Enums;
using CadastroClienteRommanel.Core.ValueObjects;

namespace CadastroClienteRommanel.Application.Tests.Tests.UsesCases.ObterClientePorIdTest.TestData;

public static class ObterClientePorIdTestData
{
    private static readonly Faker Faker = new Faker("pt_BR");

    public static Cliente ClienteFisicoValido()
    {
        var cpf = CpfCnpj.Criar("11144477735");
        var email = Email.Criar(Faker.Internet.Email());
        var telefone = Telefone.Criar(Faker.Phone.PhoneNumber("+55###########"));
        var endereco = Endereco.Criar(
            Faker.Address.ZipCode(),
            Faker.Address.StreetName(),
            Faker.Address.BuildingNumber(),
            Faker.Address.County(),
            Faker.Address.City(),
            Faker.Address.StateAbbr()
        );
        var dataNasc = Faker.Date.Past(30, DateTime.UtcNow.AddYears(-20));

        return Cliente.Criar(
            cpf,
            email,
            telefone,
            endereco,
            TipoCliente.PessoaFisica,
            ie: "isento",
            dataNascimento: dataNasc
        );
    }
}