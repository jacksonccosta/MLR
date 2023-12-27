namespace MeuLivroDeReceitas.Domain;

public interface IReceitaReadOnlyRepositorio
{
    Task<IList<Receita>> RecuperaReceitasUsuario(long usuarioId);
    Task<Receita> RecuperaReceitasPorId(long receitaId);
    Task<int> QuantidadeDeReceitas(long usuarioId);
    Task<IList<Receita>> RecuperaReceitasTodosUsuarios(List<long> usuarioIds);
}
