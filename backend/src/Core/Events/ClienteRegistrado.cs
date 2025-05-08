
namespace CadastroClienteRommanel.Core.Events;

/// <summary>
/// Disparado sempre que um Cliente é registrado com sucesso.
/// </summary>
public sealed class ClienteRegistrado
{
    public Guid ClienteId { get; }
    public DateTime OcorridoEm { get; }

    public ClienteRegistrado(Guid clienteId, DateTime ocorridoEm)
    {
        ClienteId = clienteId;
        OcorridoEm = ocorridoEm;
    }
}
