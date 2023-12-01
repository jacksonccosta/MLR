using Bogus;
using MeuLivroDeReceitas.Domain;

namespace Utilitario.Testes;

public class ReceitaBuilder
{
    public static Receita Contruir(Usuario usuario)
    {
        return new Faker<Receita>()
            .RuleFor(c => c.Id, _ => usuario.Id)
            .RuleFor(c => c.Titulo, f => f.Commerce.Department())
            .RuleFor(c => c.Categoria, f => f.PickRandom<TipoCategoria>())
            .RuleFor(c => c.ModoPreparo, f => f.Lorem.Paragraph())
            .RuleFor(c => c.Ingredientes, f => RandomIngredientes(f, usuario.Id))
            .RuleFor(c => c.UsuarioId, _ => usuario.Id);
    }

    private static List<Ingrediente> RandomIngredientes(Faker f, long usuarioId)
    {
        List<Ingrediente> ingredientes = new();

        for(int i=0; i < f.Random.Int(1, 10); i++)
        {
            ingredientes.Add(new Ingrediente()
            {
                Id = (usuarioId * 100) + (i + 1),
                Produto = f.Commerce.ProductName(),
                Quantidade = $"{f.Random.Double(1, 10)} {f.Random.Word}"
            });
        }

        return ingredientes;
    }
}
