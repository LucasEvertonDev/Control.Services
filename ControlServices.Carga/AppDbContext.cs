using InventoryControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InventoryControl.Infra.Data;
public class ApplicationDbContext : DbContext
{
    public DbSet<Usuario> Users { get; set; }

    public DbSet<Acesso> Acessos { get; set; }

    public DbSet<MapPerfilUsuariosAcessos> MapPerfilUsuariosAcessos { get; set; }

    public DbSet<Cliente> Clientes { get; set; }

    public DbSet<Servico> Servicos { get; set; }

    public DbSet<Message> Messages { get; set; }

    public DbSet<Atendimento> Atendimentos { get; set; }

    public DbSet<MapServicosAtendimento> MapServicosAtendimentos { get; set; }

    public DbSet<Custo> Custo { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Configurar a string de conexão aqui
        optionsBuilder.UseSqlServer("Data Source=NOTEBOOK\\SQLEXPRESS;Initial Catalog=InventoryControlProd;User ID=sa;Password=12345;Integrated Security=True;TrustServerCertificate=True;Connection Timeout=3600");
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}