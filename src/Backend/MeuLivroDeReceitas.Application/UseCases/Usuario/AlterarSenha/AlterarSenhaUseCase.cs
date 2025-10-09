using MeuLivroDeReceitas.Comunicacao;
using MeuLivroDeReceitas.Domain;
using MeuLivroDeReceitas.Exeptions;

namespace MeuLivroDeReceitas.Application;

public class AlterarSenhaUseCase : IAlterarSenhaUseCase
{
    private readonly IUsuarioLogado _usuarioLogado;
    private readonly IUsuarioUpdateOnlyRepositorio _repositorio;
    private readonly Encriptador _encriptador;
    private readonly IUnidadeDeTrabalho _unidadeDeTrabalho;

    public AlterarSenhaUseCase(IUsuarioUpdateOnlyRepositorio repositorio, IUsuarioLogado usuarioLogado, Encriptador encriptador, IUnidadeDeTrabalho unidadeDeTrabalho)
    {
        _repositorio = repositorio;
        _usuarioLogado = usuarioLogado;
        _encriptador = encriptador;
        _unidadeDeTrabalho = unidadeDeTrabalho;
    }

    public async Task Executar(RequestAlterarSenhaJson request)
    {
        var usuarioLogado = await _usuarioLogado.RecuperarUsuario();
        var usuario = await _repositorio.RecuperarPorId(usuarioLogado.Id);

        Validar(request, usuario);

        usuario.Senha = _encriptador.Criptografar(request.NovaSenha);

        _repositorio.Update(usuario);
        await _unidadeDeTrabalho.Commit();
    }

    private void Validar(RequestAlterarSenhaJson request, Usuario usuario) 
    {
        var validator = new AlterarSenhaValidator();
        var resultado = validator.Validate(request);

        var senhaAtualCript = _encriptador.Criptografar(request.SenhaAtual);

        if (!usuario.Senha.Equals(senhaAtualCript))
            resultado.Errors.Add(new FluentValidation.Results.ValidationFailure("senhaAtual", ResourceMensagensDeErro.SENHA_ATUAL_INVALIDA));

        if (!resultado.IsValid)
        {
            var mensagensDeErro = resultado.Errors.Select(erro => erro.ErrorMessage).ToList();
            throw new ErrosDeValidacaoException(mensagensDeErro);
        }
    }
}
