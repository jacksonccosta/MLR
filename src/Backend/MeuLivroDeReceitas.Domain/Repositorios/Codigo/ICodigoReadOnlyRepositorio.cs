namespace MeuLivroDeReceitas.Domain;

public interface ICodigoReadOnlyRepositorio
{
    Task<Codigos> GetCode(string code);
}
