using MeuLivroDeReceitas.Domain;
using MeuLivroDeReceitas.Infrastructure;
using Utilitario.Testes;

namespace WebApi.Test;

public class ContextSeedInMemory
{
    public static (Usuario usuario, string senha) Seed(MeuLivroDeReceitaContext context)
    {
        (var usuario, string senha) = UsuarioBuilder.Contruir();

        context.Usuarios.Add(usuario);
        context.SaveChanges();

        return (usuario, senha);
    }
}
