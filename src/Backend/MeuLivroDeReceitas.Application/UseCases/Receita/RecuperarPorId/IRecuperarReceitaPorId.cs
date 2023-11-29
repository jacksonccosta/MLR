using MeuLivroDeReceitas.Comunicacao;

namespace MeuLivroDeReceitas.Application;

public interface IRecuperarReceitaPorId
{
    Task<ResponseReceitaJson> Executar(long id);
}
