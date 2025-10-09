namespace MeuLivroDeReceitas.Application;

public interface IRemoverConexaoUseCase
{
    Task Executar(long idUsuarioConectado);
}
