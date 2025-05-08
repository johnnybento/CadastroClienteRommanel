
namespace CadastroClienteRommanel.Core.Exceptions;

/// <summary>
/// Exceção lançada quando uma regra de domínio é violada.
/// </summary>
public class DomainException : Exception
{
    public DomainException(string message)
        : base(message)
    {
    }
}
