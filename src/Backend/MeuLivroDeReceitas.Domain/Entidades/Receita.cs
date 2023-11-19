using MeuLivroDeReceitas.Domain.Entidades;

namespace MeuLivroDeReceitas.Domain
{
    public class Receita : EntidadeBase
    {
        public string Titulo { get; set; }
        public TipoCategoria Categoria { get; set; }
        public string ModoPreparo { get; set; }
        public ICollection<Ingrediente> Ingredientes { get; set; }
    }
}
