using MeuLivroDeReceitas.Application;
using MeuLivroDeReceitas.Domain;
using Moq;

namespace Utilitario.Testes;

public class UsuarioLogadoBuilder
{
    private static UsuarioLogadoBuilder? _instance;
    private readonly Mock<IUsuarioLogado> _repositorio;

    private UsuarioLogadoBuilder()
    {
        if (_repositorio == null)
            _repositorio = new Mock<IUsuarioLogado>();
    }


    public static UsuarioLogadoBuilder Instancia()
    {
        _instance = new UsuarioLogadoBuilder();
        return _instance;
    }

    public UsuarioLogadoBuilder RecuperarUsuario(Usuario usuario)
    {
        _repositorio.Setup(c => c.RecuperarUsuario()).ReturnsAsync(usuario);
        return this;
    }

    public IUsuarioLogado Construir()
    {
        return _repositorio.Object;
    }
}
