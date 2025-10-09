using MeuLivroDeReceitas.Domain;
using MeuLivroDeReceitas.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.Test;

public class MeuLivroDeReceitaWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{
    private Usuario? _usuario;
    private string? _senha;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Test")
            .ConfigureServices(services =>
            {
                var descritor = services.SingleOrDefault(d => d.ServiceType == typeof(MeuLivroDeReceitaContext));
                if(descritor is not null)
                    services.Remove(descritor);

                var provider =  services.AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();

                services.AddDbContext<MeuLivroDeReceitaContext>(option =>
                {
                    option.UseInMemoryDatabase("InMemoryDbForTesting");
                    option.UseInternalServiceProvider(provider);
                });

                var serviceProvider = services.BuildServiceProvider();

                using var scope = serviceProvider.CreateScope();
                var scopeService = scope.ServiceProvider;
                var dataBase = scopeService.GetRequiredService<MeuLivroDeReceitaContext>();

                dataBase.Database.EnsureDeleted();

                (_usuario, _senha) = ContextSeedInMemory.Seed(dataBase);
            });
    }

#pragma warning disable CS8603 // Possible null reference return.
    public Usuario RecuperarUsuario() => _usuario;

    public string RecuperarSenha() => _senha;
#pragma warning restore CS8603 // Possible null reference return.
}
