using MeuLivroDeReceitas.Domain;
using Moq;

namespace Utilitario.Testes.Repositorios;

public class UnidadeDeTrabalhoBuilder
{
    private static UnidadeDeTrabalhoBuilder _instance;
    private Mock<IUnidadeDeTrabalho> _repositorio;

    private UnidadeDeTrabalhoBuilder()
    {
        if (_repositorio == null)
            _repositorio = new Mock<IUnidadeDeTrabalho>();
    }


    public static UnidadeDeTrabalhoBuilder Instancia()
    {
        _instance = new UnidadeDeTrabalhoBuilder();
        return _instance;
    }

    public IUnidadeDeTrabalho Construir()
    {
        return _repositorio.Object;
    }
}
