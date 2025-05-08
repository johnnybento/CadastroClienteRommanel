
using CadastroClienteRommanel.Core.Entities;
using Microsoft.EntityFrameworkCore;


namespace CadastroClienteRommanel.Adapters.Driven.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public DbSet<Cliente> Clientes { get; set; } = default!;
    public DbSet<EventEntry> Events { get; set; } = default!;

    public AppDbContext(DbContextOptions<AppDbContext> opts)
        : base(opts)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       
        modelBuilder.Entity<Cliente>(b =>
        {
            b.HasKey(c => c.Id);
            b.OwnsOne(c => c.CpfCnpj, vo => vo.Property(v => v.Value).HasColumnName("CpfCnpj"));
            b.OwnsOne(c => c.Email, vo => vo.Property(v => v.Value).HasColumnName("Email"));
            b.OwnsOne(c => c.Telefone, vo => vo.Property(v => v.Numero).HasColumnName("Telefone"));
            b.OwnsOne(c => c.Endereco, vo =>
            {
                vo.Property(v => v.CEP).HasColumnName("Cep");
                vo.Property(v => v.Logradouro).HasColumnName("Logradouro");
                vo.Property(v => v.Numero).HasColumnName("Numero");
                vo.Property(v => v.Bairro).HasColumnName("Bairro");
                vo.Property(v => v.Cidade).HasColumnName("Cidade");
                vo.Property(v => v.Estado).HasColumnName("Estado");
            });
            b.Property(c => c.Tipo).HasConversion<string>().HasColumnName("TipoCliente");
            b.Property(c => c.Ie).HasMaxLength(20);
            b.Property(c => c.DataNascimento);
            b.Property(c => c.DataRegistro);
        });

  
        modelBuilder.Entity<EventEntry>(b =>
        {
            b.ToTable("Events");
            b.HasKey(e => e.Id);
            b.Property(e => e.AggregateId).IsRequired();
            b.Property(e => e.Type).HasMaxLength(200).IsRequired();
            b.Property(e => e.Data).IsRequired();
            b.Property(e => e.OccurredOn).IsRequired();
        });
    }
}
