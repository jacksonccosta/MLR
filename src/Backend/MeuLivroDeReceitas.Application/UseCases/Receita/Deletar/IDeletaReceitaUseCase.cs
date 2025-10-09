namespace MeuLivroDeReceitas.Application;

public interface IDeletaReceitaUseCase
{
    Task Executar(long receitaId);
}
