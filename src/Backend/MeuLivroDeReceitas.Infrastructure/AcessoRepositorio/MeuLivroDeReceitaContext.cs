using MeuLivroDeReceitas.Domain;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace MeuLivroDeReceitas.Infrastructure;

public class MeuLivroDeReceitaContext : DbContext
{
    public MeuLivroDeReceitaContext(DbContextOptions<MeuLivroDeReceitaContext> options) : base(options) {}

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Receita> Receitas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MeuLivroDeReceitaContext).Assembly);
    }
}
