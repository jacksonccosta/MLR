using MeuLivroDeReceitas.Domain;
using MeuLivroDeReceitas.Infrastructure;
using Utilitario.Testes;

namespace WebApi.Test;

public class ContextSeedInMemory
{
    public static (Usuario usuario, string senha) Seed(MeuLivroDeReceitaContext context)
    {
        (var usuario, string senha) = UsuarioBuilder.Construir();
        var receita = ReceitaBuilder.Contruir(usuario);

        context.Usuarios.Add(usuario);
        context.Receitas.Add(receita);

        context.SaveChanges();

        return (usuario, senha);
    }

    public static (Usuario usuario, string senha) SeedUsuarioSemReceita(MeuLivroDeReceitaContext context)
    {
        (var usuario, string senha) = UsuarioBuilder.Construir();

        context.Usuarios.Add(usuario);

        context.SaveChanges();

        return (usuario, senha);
    }
}
