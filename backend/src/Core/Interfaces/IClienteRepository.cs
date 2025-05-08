
using CadastroClienteRommanel.Core.Entities;
using CadastroClienteRommanel.Core.ValueObjects;

namespace CadastroClienteRommanel.Core.Interfaces;

/// <summary>
/// Contrato para persistência de Clientes.
/// </summary>
public interface IClienteRepository
{
    /// <summary>
    /// Verifica se já existe um cliente cadastrado com o mesmo CPF/CNPJ.
    /// </summary>
    Task<bool> ExistePorCpfCnpjAsync(CpfCnpj cpfCnpj);

    /// <summary>
    /// Verifica se já existe um cliente cadastrado com o mesmo e-mail.
    /// </summary>
    Task<bool> ExistePorEmailAsync(Email email);

    /// <summary>
    /// Adiciona um novo cliente ao repositório.
    /// </summary>
    Task AdicionarAsync(Cliente cliente);

    /// <summary>
    /// Recupera um cliente por seu identificador.
    /// </summary>
    Task<Cliente?> ObterPorIdAsync(Guid id);
}