namespace MeuLivroDeReceitas.Domain;

public class EntidadeBase
{
    public long Id { get; set; }
    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
    public DateTime DataModificacao { get; set; } = DateTime.UtcNow;
    public byte indAtivo { get; set; } = 1;
}
