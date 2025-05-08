using CadastroClienteRommanel.Core.Enums;
using FluentValidation;

namespace CadastroClienteRommanel.Application.UseCases.RegistrarCliente
{
    public class RegistrarClienteValidator : AbstractValidator<RegistrarClienteCommand>
    {
        public RegistrarClienteValidator()
        {
            RuleFor(x => x.CpfCnpj)
                .NotEmpty().WithMessage("CPF/CNPJ é obrigatório.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("E-mail é obrigatório.")
                .EmailAddress().WithMessage("Formato de e-mail inválido.");

            RuleFor(x => x.Telefone)
                .NotEmpty().WithMessage("Telefone é obrigatório.");

            RuleFor(x => x.Cep)
                .NotEmpty().WithMessage("CEP é obrigatório.");

            RuleFor(x => x.Logradouro)
                .NotEmpty().WithMessage("Logradouro é obrigatório.");

            RuleFor(x => x.Numero)
                .NotEmpty().WithMessage("Número é obrigatório.");

            RuleFor(x => x.Bairro)
                .NotEmpty().WithMessage("Bairro é obrigatório.");

            RuleFor(x => x.Cidade)
                .NotEmpty().WithMessage("Cidade é obrigatória.");

            RuleFor(x => x.Estado)
                .NotEmpty().WithMessage("Estado é obrigatório.");

            RuleFor(x => x.Tipo)
                .IsInEnum().WithMessage("Tipo de cliente inválido.");

            When(x => x.Tipo == TipoCliente.PessoaFisica, () =>
            {
                RuleFor(x => x.DataNascimento)
                    .NotNull().WithMessage("Data de nascimento é obrigatória para pessoa física.");
            });

            When(x => x.Tipo == TipoCliente.PessoaJuridica, () =>
            {
                RuleFor(x => x.Ie)
                    .NotEmpty().WithMessage("IE ou 'isento' é obrigatório para pessoa jurídica.");
            });
        }
    }
}
