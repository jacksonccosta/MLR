using MeuLivroDeReceitas.Domain;

namespace MeuLivroDeReceitas.Application;

public interface IUsuarioLogado
{
    Task<Usuario> RecuperarUsuario();
}
