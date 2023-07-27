using MeuLivroDeReceitas.Comunicacao;
using MeuLivroDeReceitas.Domain;
using MeuLivroDeReceitas.Exeptions.ExecptionsBase;

namespace MeuLivroDeReceitas.Application;

public class LoginUseCase : ILoginUseCase
{
    private readonly IUsuarioReadOnlyRepositorio _usuarioReadyOnlyRepositorio;
    private readonly Encriptador _encript;
    private readonly TokenController _token;

    public LoginUseCase(IUsuarioReadOnlyRepositorio usuarioReadyOnlyRepositorio, Encriptador encript, TokenController token)
    {
        _usuarioReadyOnlyRepositorio = usuarioReadyOnlyRepositorio;
        _encript = encript;
        _token = token;
    }
    public async Task<ResponseLoginJson> Executar(RequestLoginJson request)
    {
        var senhaCriptografada = _encript.Criptografar(request.Senha);

        var usuario = await _usuarioReadyOnlyRepositorio.Login(request.Email, senhaCriptografada);

        if(usuario == null)
            throw new LoginInvalidoException();

        return new ResponseLoginJson
        {
            Nome = usuario.Nome,
            Token = _token.GerarToken(usuario.Email)
        };

    }
}
