using MeuLivroDeReceitas.Domain;
using MeuLivroDeReceitas.Exeptions;

namespace MeuLivroDeReceitas.Application;

public class DeletaReceitaUseCase : IDeletaReceitaUseCase
{

    private readonly IUsuarioLogado _usuarioLogado;
    private readonly IUnidadeDeTrabalho _unidadeDeTrabalho;
    private readonly IReceitaWriteOnlyRepositorio _repositorioWriteOnly;
    private readonly IReceitaReadOnlyRepositorio _repositorioReadOnly;

    public DeletaReceitaUseCase(
        IUsuarioLogado usuarioLogado, 
        IUnidadeDeTrabalho unidadeDeTrabalho, 
        IReceitaReadOnlyRepositorio repositorioReadOnly, 
        IReceitaWriteOnlyRepositorio repositorioWriteOnly
    )
    {
        _usuarioLogado = usuarioLogado;
        _unidadeDeTrabalho = unidadeDeTrabalho;
        _repositorioReadOnly = repositorioReadOnly;
        _repositorioWriteOnly = repositorioWriteOnly;
    }

    public async Task Executar(long receitaId)
    {
        var usuarioLogado = await _usuarioLogado.RecuperarUsuario();
        var receita = await _repositorioReadOnly.RecuperaReceitasPorId(receitaId);

        Validar(usuarioLogado, receita);

        await _repositorioWriteOnly.Deletar(receitaId);
        await _unidadeDeTrabalho.Commit();
    }

    private static void Validar(Usuario usuarioLogado, Receita receita)
    {
        if (receita is null || receita.UsuarioId != usuarioLogado.Id)
            throw new ErrosDeValidacaoException(new List<string>() { ResourceMensagensDeErro.RECEITA_NAO_ENCONTRADA });
    }
}
