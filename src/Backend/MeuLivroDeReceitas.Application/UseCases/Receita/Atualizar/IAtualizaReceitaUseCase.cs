using MeuLivroDeReceitas.Comunicacao;

namespace MeuLivroDeReceitas.Application;

public interface IAtualizaReceitaUseCase
{
    Task Executar(long id, RequestReceitaJson request);
}
