namespace MeuLivroDeReceitas.Domain;
public interface IUsuarioReadOnlyRepositorio
{
    Task<bool> ExisteUsuarioComEmail(string email);
}
