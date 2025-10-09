namespace MeuLivroDeReceitas.Domain;

public interface ICodigoWriteOnlyRepositorio
{
    Task Registrar(Codigos codigo);
    Task Deletar(long usuarioId);
}
