using MeuLivroDeReceitas.Application;
using MeuLivroDeReceitas.Comunicacao;
using MeuLivroDeReceitas.Domain;
using MeuLivroDeReceitas.Exeptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;

namespace MeuLivroDeReceitas.Api.Filtros;

public class UsuarioAutenticadoAttribute : AuthorizeAttribute, IAsyncAuthorizationFilter
{
    private readonly TokenController _tokenController;
    private readonly IUsuarioReadOnlyRepositorio _repositorio;

    public UsuarioAutenticadoAttribute(TokenController tokenController, IUsuarioReadOnlyRepositorio repositorio)
    {
        _tokenController = tokenController;
        _repositorio = repositorio;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        try
        {
            var token = TokenInRequest(context);
            var email = _tokenController.RecuperarEmail(token);

            var usuario = await _repositorio.RecuperarPorEmail(email);

            if (usuario is null)
                throw new System.Exception();
        }
        catch (SecurityTokenExpiredException)
        {
            TokenExpirado(context);
        }
        catch 
        {
            UsuarioNaoAutorizado(context);
        }
    }

    private string TokenInRequest(AuthorizationFilterContext context)
    {
        var authorization = context.HttpContext.Request.Headers["Authorization"].ToString();

        if (string.IsNullOrWhiteSpace(authorization))
            throw new System.Exception();

        return authorization["Bearer".Length..].Trim();
    }

    private void TokenExpirado(AuthorizationFilterContext context)
    {
        context.Result = new UnauthorizedObjectResult(new RespostaErroJson(ResourceMensagensDeErro.TOKEN_EXPIRADO));
    }

    private void UsuarioNaoAutorizado(AuthorizationFilterContext context)
    {
        context.Result = new UnauthorizedObjectResult(new RespostaErroJson(ResourceMensagensDeErro.USUARIO_NAO_AUTORIZADO));
    }
}
