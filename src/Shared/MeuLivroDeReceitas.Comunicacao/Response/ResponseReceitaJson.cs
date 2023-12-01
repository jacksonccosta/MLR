namespace MeuLivroDeReceitas.Comunicacao;

public class ResponseReceitaJson
{
    public ResponseReceitaJson()
    {
        Ingredientes = new();
    }
    public string Id { get; set; }
    public string Titulo { get; set; }
    public TipoCategoria Categoria { get; set; }
    public string ModoPreparo { get; set; }
    public int TempoPreparo { get; set; }
    public List<ResponseIngredientesJson> Ingredientes { get; set; }
}

