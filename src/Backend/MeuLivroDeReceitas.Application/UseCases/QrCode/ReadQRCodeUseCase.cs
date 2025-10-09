using HashidsNet;
using MeuLivroDeReceitas.Comunicacao;
using MeuLivroDeReceitas.Domain;
using MeuLivroDeReceitas.Exeptions;

namespace MeuLivroDeReceitas.Application;

public class ReadQRCodeUseCase : IReadQRCodeUseCase
{
    private readonly IHashids _hashId;
    private readonly IConexaoReadOnlyRepositorio _repositorioConexao;
    private readonly ICodigoReadOnlyRepositorio _repositorio;
    private readonly IUsuarioLogado _usuarioLogado;

    public ReadQRCodeUseCase(ICodigoReadOnlyRepositorio repositorio, IUsuarioLogado usuarioLogado, IConexaoReadOnlyRepositorio repositorioConexao, IHashids hashId)
    {
        _repositorio = repositorio;
        _usuarioLogado = usuarioLogado;
        _repositorioConexao = repositorioConexao;
        _hashId = hashId;
    }

    public async Task<(ResponseUsuarioConexaoJson usuarioToConect, string idUsuarioQrCodeGerado)> Execute(string codeConnection)
    {
        var usuarioLogado = await _usuarioLogado.RecuperarUsuario();
        var code = await _repositorio.GetCode(codeConnection);

        await Validar(code, usuarioLogado);

        var usuarioToConect = new ResponseUsuarioConexaoJson
        {
            Id = _hashId.EncodeLong(usuarioLogado.Id),
            Nome = usuarioLogado.Nome
        };

        return (usuarioToConect, _hashId.EncodeLong(code.UsuarioId));
    }

    private async Task Validar(Codigos code, Usuario usuarioLogado)
    {
        if(code is null)
            throw new MeuLivroDeReceitasException("");

        if (code.UsuarioId == usuarioLogado.Id)
            throw new MeuLivroDeReceitasException("");

        var existeConexao = await _repositorioConexao.ExisteConexao(code.UsuarioId, usuarioLogado.Id);
        if(existeConexao)
            throw new MeuLivroDeReceitasException("");
    }
}
