using MeuLivroDeReceitas.Comunicacao;

namespace MeuLivroDeReceitas.Application;

public interface IDashboardUseCase
{
    Task<ResponseDashboardJson> Executar(RequestDashboardJson request);
}
