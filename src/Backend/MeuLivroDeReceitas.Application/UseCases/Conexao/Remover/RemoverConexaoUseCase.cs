using MeuLivroDeReceitas.Domain;
using MeuLivroDeReceitas.Exeptions;
using Microsoft.AspNetCore.Components.Forms;

namespace MeuLivroDeReceitas.Application;

public class RemoverConexaoUseCase : IRemoverConexaoUseCase
{
    private readonly IUsuarioLogado _usuarioLogado;
    private readonly IConexaoWriteOnlyRepositorio _repositorioWriteOnly;
    private readonly IConexaoReadOnlyRepositorio _repositorioReadOnly;
    private readonly IUnidadeDeTrabalho _unidadeDeTrabalho;

    public RemoverConexaoUseCase(IUsuarioLogado usuarioLogado,
                                IConexaoWriteOnlyRepositorio repositorioWriteOnly,
                                IConexaoReadOnlyRepositorio repositorioReadOnly,
                                IUnidadeDeTrabalho unidadeDeTrabalho)
    {
        _usuarioLogado = usuarioLogado;
        _repositorioWriteOnly = repositorioWriteOnly;
        _repositorioReadOnly = repositorioReadOnly;
        _unidadeDeTrabalho = unidadeDeTrabalho;
    }

    public async Task Executar(long idUsuarioParaRemover)
    {
        var usuarioLogado = await _usuarioLogado.RecuperarUsuario();

        var usuarioConectados = await _repositorioReadOnly.RecuperarDoUsuario(usuarioLogado.Id);

        Validar(usuarioConectados, idUsuarioParaRemover);

        await _repositorioWriteOnly.RemoverConexao(usuarioLogado.Id, idUsuarioParaRemover);

        await _unidadeDeTrabalho.Commit();
    }

    private void Validar(IList<Usuario> usuariosConectados, long idUsuarioConectadoParaRemover)
    {
        if (!usuariosConectados.Any(c => c.Id == idUsuarioConectadoParaRemover))
            throw new ErrosDeValidacaoException(new List<string> { "" });
    }
}
