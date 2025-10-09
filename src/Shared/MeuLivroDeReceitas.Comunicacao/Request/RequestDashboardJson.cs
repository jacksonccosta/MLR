namespace MeuLivroDeReceitas.Comunicacao;

public class RequestDashboardJson
{
    public string TituloOuIngrediente { get; set; }
    public TipoCategoria? Categoria { get; set; }
}
