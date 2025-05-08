using Bogus;
using CadastroClienteRommanel.Application.UseCases.RegistrarCliente;
using CadastroClienteRommanel.Core.Enums;

namespace CadastroClienteRommanel.Application.Tests.Tests.UsesCases.RegistrarCliente.TestData;

public static class RegistrarClienteTestData
{
    private static readonly Faker Faker = new("pt_BR");

    public static RegistrarClienteCommand ComandoValidoPF()
        => new Faker<RegistrarClienteCommand>("pt_BR")
            .CustomInstantiator(f => new RegistrarClienteCommand(
                cpfCnpj: "11144477735",
                email: f.Internet.Email(),
                telefone: f.Phone.PhoneNumber("+55###########"),
                cep: f.Address.ZipCode(),
                logradouro: f.Address.StreetName(),
                numero: f.Address.BuildingNumber(),
                bairro: f.Address.County(),
                cidade: f.Address.City(),
                estado: f.Address.StateAbbr(),
                tipo: TipoCliente.PessoaFisica,
                ie: "isento",
                dataNascimento: f.Date.Past(30, DateTime.UtcNow.AddYears(-20))
            ));
}
