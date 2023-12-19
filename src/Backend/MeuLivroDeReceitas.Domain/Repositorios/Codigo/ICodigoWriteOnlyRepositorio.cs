namespace MeuLivroDeReceitas.Domain;

public interface ICodigoWriteOnlyRepositorio
{
    Task Registrar(Codigos codigo);
}
