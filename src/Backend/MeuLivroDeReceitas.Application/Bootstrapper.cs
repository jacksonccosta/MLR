using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MeuLivroDeReceitas.Application;

public static class Bootstrapper
{
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        AdicionarChaveAdicionalSenha(services, configuration);
        AdicionarTokenJWT(services, configuration);
        AdicionarUseCases(services);
        AdicionarUsuarioLogado(services);
    }

    private static void AdicionarUsuarioLogado(IServiceCollection services)
    {
        services.AddScoped<IUsuarioLogado, UsuarioLogado>();
    }

    private static void AdicionarChaveAdicionalSenha(IServiceCollection services, IConfiguration configuration)
    {
        var section = configuration.GetRequiredSection("Configuracoes:Senha:ChaveAdicionalDeSenha");
        services.AddScoped(option => new Encriptador(section.Value));
    }

    private static void AdicionarTokenJWT(IServiceCollection services, IConfiguration configuration)
    {
        var sectionTempoDeVidaToken = configuration.GetRequiredSection("Configuracoes:JWT:TempoDeVidaTokenEmMinutos");
        var sectionChaveToken = configuration.GetRequiredSection("Configuracoes:JWT:ChaveToken");
        services.AddScoped(option => new TokenController(int.Parse(sectionTempoDeVidaToken.Value), sectionChaveToken.Value));
    }

    private static void AdicionarUseCases(IServiceCollection services)
    {
        services.AddScoped<IRegistrarUsuarioUseCase, RegistrarUsuarioUseCase>()
                .AddScoped<ILoginUseCase, LoginUseCase>()
                .AddScoped<IAlterarSenhaUseCase, AlterarSenhaUseCase>();
    }
}
