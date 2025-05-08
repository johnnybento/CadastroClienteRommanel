
namespace CadastroClienteRommanel.Adapters.Driven.Infrastructure.Persistence;

/// <summary>
/// Representa um evento de domínio gravado para auditabilidade e Event Sourcing.
/// </summary>
public class EventEntry
{
    public Guid Id { get; set; }
    public Guid AggregateId { get; set; }
    public string Type { get; set; } = default!;
    public string Data { get; set; } = default!;
    public DateTime OccurredOn { get; set; }
}