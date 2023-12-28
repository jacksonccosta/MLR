using MeuLivroDeReceitas.Domain;
using Moq;

namespace Utilitario.Testes;

public class UsuarioWriteOnlyRepositorioBuilder
{
    private static UsuarioWriteOnlyRepositorioBuilder? _instance;
    private readonly Mock<IUsuarioWriteOnlyRepositorio> _repositorio;

    private UsuarioWriteOnlyRepositorioBuilder()
    {
        if (_repositorio is null)
            _repositorio = new Mock<IUsuarioWriteOnlyRepositorio>();
    }


    public static UsuarioWriteOnlyRepositorioBuilder Instancia()
    {
        _instance = new UsuarioWriteOnlyRepositorioBuilder();
        return _instance;
    }

    public IUsuarioWriteOnlyRepositorio Construir()
    {
        return _repositorio.Object;
    }
}
