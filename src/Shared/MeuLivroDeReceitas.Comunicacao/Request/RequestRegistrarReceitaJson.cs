namespace MeuLivroDeReceitas.Comunicacao
{
    public class RequestRegistrarReceitaJson
    {
        public RequestRegistrarReceitaJson()
        {
            Ingredientes = new();
        }
        public string Titulo { get; set; }
        public TipoCategoria Categoria { get; set; }
        public string ModoPreparo { get; set; }
        public List<RequestRegistrarIngredienteJson> Ingredientes { get; set; }
    }
}
