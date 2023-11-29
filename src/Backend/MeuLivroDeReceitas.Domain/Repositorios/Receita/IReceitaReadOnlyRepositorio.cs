namespace MeuLivroDeReceitas.Domain;

public interface IReceitaReadOnlyRepositorio
{
    Task<List<Receita>> RecuperaReceitasUsuario(long usuarioId);
    Task<Receita> RecuperaReceitasPorId(long receitaId);
}
