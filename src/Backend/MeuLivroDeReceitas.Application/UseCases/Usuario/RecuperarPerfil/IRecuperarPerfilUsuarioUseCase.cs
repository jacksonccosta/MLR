using MeuLivroDeReceitas.Comunicacao;

namespace MeuLivroDeReceitas.Application;

public interface IRecuperarPerfilUsuarioUseCase
{
    Task<ResponsePerfilUsuarioJson> Executar();
}
