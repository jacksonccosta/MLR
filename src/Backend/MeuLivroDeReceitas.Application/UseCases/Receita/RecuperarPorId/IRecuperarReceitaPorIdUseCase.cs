using MeuLivroDeReceitas.Comunicacao;

namespace MeuLivroDeReceitas.Application;

public interface IRecuperarReceitaPorIdUseCase
{
    Task<ResponseReceitaJson> Executar(long id);
}
