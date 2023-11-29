namespace MeuLivroDeReceitas.Domain;

public interface IReceitaReadOnlyRepositorio
{
    Task<IList<Receita>> RecuperaReceitasUsuario(long usuarioId);
    Task<Receita> RecuperaReceitasPorId(long receitaId);
}
