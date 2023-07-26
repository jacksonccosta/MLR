namespace MeuLivroDeReceitas.Domain;
public interface IUsuarioReadyOnlyRepositorio
{
    Task<bool> ExisteUsuarioComEmail(string email);
}
