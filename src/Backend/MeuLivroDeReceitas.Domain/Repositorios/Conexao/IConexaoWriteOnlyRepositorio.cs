namespace MeuLivroDeReceitas.Domain;

public interface IConexaoWriteOnlyRepositorio
{
    Task Registrar(Conexoes conexao);
}
