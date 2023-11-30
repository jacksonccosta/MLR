using FluentMigrator.Runner;
using MeuLivroDeReceitas.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace MeuLivroDeReceitas.Infrastructure;

public static class Bootstrapper
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configurationManager)
    {
        AddFluentMigrator(services, configurationManager);

        AddContexto(services, configurationManager);
        AddUnidadeDeTrabalho(services);
        AddRepositorios(services);
    }

    private static void AddContexto(IServiceCollection services, IConfiguration configurationManager)
    {
        _ = bool.TryParse(configurationManager.GetSection("Configuracoes:DataBaseInMemory").Value, out bool dataBaseInMemory);

        if (!dataBaseInMemory) 
        {
            var versaoServidor = new MySqlServerVersion(new Version(8, 1, 0));
            var connectionString = configurationManager.GetConexaoCompleta();

            services.AddDbContext<MeuLivroDeReceitaContext>(dbContextoOpcoes =>
            {
                dbContextoOpcoes.UseMySql(connectionString, versaoServidor);
            });
        }        
    }

    private static void AddUnidadeDeTrabalho(IServiceCollection services)
    {
        services.AddScoped<IUnidadeDeTrabalho, UnidadeDeTrabalho>();
    }

    private static void AddRepositorios(IServiceCollection services)
    {
        services.AddScoped<IUsuarioWriteOnlyRepositorio, UsuarioRepositorio>()
                .AddScoped<IUsuarioReadOnlyRepositorio, UsuarioRepositorio>()
                .AddScoped<IUsuarioUpdateOnlyRepositorio, UsuarioRepositorio>()
                .AddScoped<IReceitaWriteOnlyRepositorio, ReceitaRepositorio>()
                .AddScoped<IReceitaReadOnlyRepositorio, ReceitaRepositorio>()
                .AddScoped<IReceitaUpdateOnlyRepositorio, ReceitaRepositorio>();
    }

    private static void AddFluentMigrator(IServiceCollection services, IConfiguration configurationManager)
    {
        _ = bool.TryParse(configurationManager.GetSection("Configuracoes:DataBaseInMemory").Value, out bool dataBaseInMemory);

        if (!dataBaseInMemory)
            services.AddFluentMigratorCore().ConfigureRunner(c => 
                                                         c.AddMySql5()
                                                         .WithGlobalConnectionString(configurationManager.GetConexaoCompleta()).ScanIn(Assembly.Load("MeuLivroDeReceitas.Infrastructure")).For.All()
                                                         );
    }
}
