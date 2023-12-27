namespace MeuLivroDeReceitas.Domain;

public interface IConexaoWriteOnlyRepositorio
{
    Task Registrar(Conexoes conexao);
    Task RemoverConexao(long usuarioId, long usuarioIdParaRemover);
}
