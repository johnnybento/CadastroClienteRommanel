using CadastroClienteRommanel.Core.Entities;
using CadastroClienteRommanel.Core.Enums;
using CadastroClienteRommanel.Core.ValueObjects;

namespace CadastroClienteRommanel.Core.Tests.Tests.Entities.TestData;

public static class ClienteTestData
{
    public static Cliente ClienteFisicoValido()
    {
        var cpf = CpfCnpj.Criar("11144477735");
        var email = Email.Criar("teste@exemplo.com");
        var tel = Telefone.Criar("+5511999999999");
        var end = Endereco.Criar("12345-678", "Rua A", "100", "Bairro B", "Cidade C", "SP");
        var nasc = DateTime.UtcNow.AddYears(-25);

        return Cliente.Criar(
            cpf, email, tel, end,
            TipoCliente.PessoaFisica,
            ie: "isento",
            dataNascimento: nasc
        );
    }

    public static Cliente ClienteJuridicoValido()
    {
        var cnpj = CpfCnpj.Criar("11222333000181");
        var email = Email.Criar("pj@exemplo.com");
        var tel = Telefone.Criar("+5511988888888");
        var end = Endereco.Criar("87654-321", "Av. X", "200", "Bairro Y", "Cidade Z", "RJ");

        return Cliente.Criar(
            cnpj, email, tel, end,
            TipoCliente.PessoaJuridica,
            ie: "123456789",
            dataNascimento: null
        );
    }
}

