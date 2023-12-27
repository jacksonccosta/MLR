using MeuLivroDeReceitas.Comunicacao;

namespace MeuLivroDeReceitas.Application;

public interface IRecuperarTodasAsConexoesUseCase
{
    Task<IList<ResponseUsuarioConectadoJson>> Executar();
}
