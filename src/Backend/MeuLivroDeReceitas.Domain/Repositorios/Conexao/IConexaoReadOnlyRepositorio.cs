namespace MeuLivroDeReceitas.Domain;

public interface IConexaoReadOnlyRepositorio
{
    Task<bool> ExisteConexao(long codeUserOne, long codeUserTwo);
}
