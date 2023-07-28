using MeuLivroDeReceitas.Comunicacao;

namespace MeuLivroDeReceitas.Application;

public interface ILoginUseCase
{
    Task<ResponseLoginJson> Executar(RequestLoginJson request);
}
