using MeuLivroDeReceitas.Domain;
using Microsoft.AspNetCore.Http;

namespace MeuLivroDeReceitas.Application;

public class UsuarioLogado : IUsuarioLogado
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly TokenController _tokenController;
    private readonly IUsuarioReadOnlyRepositorio _repositorio;

    public UsuarioLogado(IHttpContextAccessor httpContextAccessor, TokenController tokenController, IUsuarioReadOnlyRepositorio repositorio)
    {
        _httpContextAccessor = httpContextAccessor;
        _tokenController = tokenController;
        _repositorio = repositorio;
    }

    public async Task<Usuario> RecuperarUsuario()
    {
        var authorization =  _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();

        var token = authorization["Bearer".Length..].Trim();
        var emailUsuario = _tokenController.RecuperarEmail(token);

        var usuario = await _repositorio.RecuperarEmail(emailUsuario);

        return usuario;
    }
}
