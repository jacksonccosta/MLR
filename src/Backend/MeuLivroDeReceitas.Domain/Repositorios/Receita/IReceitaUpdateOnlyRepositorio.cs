namespace MeuLivroDeReceitas.Domain;

public interface IReceitaUpdateOnlyRepositorio
{
    Task<Receita> RecuperaReceitasPorId(long receitaId);
    void Update(Receita receita);
}
