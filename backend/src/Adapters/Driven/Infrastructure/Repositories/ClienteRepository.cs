
using CadastroClienteRommanel.Adapters.Driven.Infrastructure.Persistence;
using CadastroClienteRommanel.Core.Entities;
using CadastroClienteRommanel.Core.Interfaces;
using CadastroClienteRommanel.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace CadastroClienteRommanel.Adapters.Driven.Infrastructure.Repositories;

public class ClienteRepository : IClienteRepository
{
    private readonly AppDbContext _context;

    public ClienteRepository(AppDbContext context)
        => _context = context;

    public async Task<bool> ExistePorCpfCnpjAsync(CpfCnpj cpfCnpj)
        => await _context.Clientes
            .AnyAsync(c => c.CpfCnpj.Value == cpfCnpj.Value);

    public async Task<bool> ExistePorEmailAsync(Email email)
        => await _context.Clientes
            .AnyAsync(c => c.Email.Value == email.Value);

    public async Task AdicionarAsync(Cliente cliente)
    {
        _context.Clientes.Add(cliente);

        foreach (var evt in cliente.EventosDominio)
        {
            var entry = new EventEntry
            {
                Id = Guid.NewGuid(),
                AggregateId = cliente.Id,
                Type = evt.GetType().Name,
                Data = JsonSerializer.Serialize(evt),
                OccurredOn = DateTime.UtcNow
            };
            _context.Events.Add(entry);
        }

        await _context.SaveChangesAsync();
    }

    public async Task<Cliente?> ObterPorIdAsync(Guid id)
        => await _context.Clientes.FindAsync(id);
}