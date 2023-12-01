namespace MeuLivroDeReceitas.Comunicacao
{
    public class RequestReceitaJson
    {
        public RequestReceitaJson()
        {
            Ingredientes = new();
        }
        public string Titulo { get; set; }
        public TipoCategoria Categoria { get; set; }
        public string ModoPreparo { get; set; }
        public int TempoPreparo { get; set; }
        public List<RequestIngredienteJson> Ingredientes { get; set; }
    }
}
