namespace MeuLivroDeReceitas.Domain;
public interface IUsuarioReadOnlyRepositorio
{
    Task<bool> ExisteUsuarioComEmail(string email);
    Task<Usuario> Login(string email, string senha);
}
