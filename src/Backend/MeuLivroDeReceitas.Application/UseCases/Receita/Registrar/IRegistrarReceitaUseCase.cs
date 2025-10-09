using MeuLivroDeReceitas.Comunicacao;

namespace MeuLivroDeReceitas.Application
{
    public interface IRegistrarReceitaUseCase
    {
        Task<ResponseReceitaJson> Executar(RequestReceitaJson request);
    }
}
