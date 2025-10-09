using MeuLivroDeReceitas.Comunicacao;

namespace MeuLivroDeReceitas.Application;

public interface IRegistrarUsuarioUseCase
{
    Task<ResponseUsuarioRegistradoJson> Executar(RequestRegistrarUsuarioJson request);
}
