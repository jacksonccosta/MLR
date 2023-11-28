using Bogus;
using MeuLivroDeReceitas.Comunicacao;

namespace Utilitario.Testes;

public class RequestRegistrarReceitaBuilder
{
    public static RequestRegistrarReceitaJson Construir()
    {
        return new Faker<RequestRegistrarReceitaJson>()
            .RuleFor(c => c.Titulo, f => f.Commerce.Department())
            .RuleFor(c => c.Categoria, f => f.PickRandom<TipoCategoria>())
            .RuleFor(c => c.ModoPreparo, f => f.Lorem.Paragraph())
            .RuleFor(c => c.Ingredientes, f => RandomIngredientes(f));
            //.RuleFor(c => c.TempoPreparo, f => f.Random.Int(1, 1000));
    }

    private static List<RequestRegistrarIngredienteJson> RandomIngredientes(Faker f)
    {
        List<RequestRegistrarIngredienteJson> ingredientes = new();

        for (int i = 0; i < f.Random.Int(1, 10); i++)
        {
            ingredientes.Add(new RequestRegistrarIngredienteJson
            {
                Produto = f.Commerce.ProductName(),
                Quantidade = $"{f.Random.Double(1, 10)} {f.Random.Word()}"
            });
        }

        return ingredientes;
    }
}
