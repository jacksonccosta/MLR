namespace MeuLivroDeReceitas.Domain;

public interface IUsuarioUpdateOnlyRepositorio
{
    void Update(Domain.Usuario usuario);
    Task<Usuario> RecuperarPorId(long id);
}
