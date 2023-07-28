using MeuLivroDeReceitas.Comunicacao;

namespace MeuLivroDeReceitas.Application;

public interface IAlterarSenhaUseCase
{
    Task Executar(RequestAlterarSenhaJson request);
}
