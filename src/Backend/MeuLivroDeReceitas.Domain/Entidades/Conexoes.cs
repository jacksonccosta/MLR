namespace MeuLivroDeReceitas.Domain;

public class Conexoes : EntidadeBase
{
    public long UsuarioId { get; set; }
    public long ConexaoUsuarioId { get; set; }
    public Usuario ConectadoComUsuario { get; set; }
}
