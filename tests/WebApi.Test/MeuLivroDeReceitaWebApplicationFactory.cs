﻿using MeuLivroDeReceitas.Domain;
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
                if(descritor != null)
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

    public Usuario RecuperarUsuario()
    {
        return _usuario;
    }

    public string RecuperarSenha()
    {
        return _senha;
    }
}
