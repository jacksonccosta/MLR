namespace MeuLivroDeReceitas.Application;

public interface IAceitarConexaoUseCase
{
    Task<string> Executar(string usuarioParaSeConectarId);
}
