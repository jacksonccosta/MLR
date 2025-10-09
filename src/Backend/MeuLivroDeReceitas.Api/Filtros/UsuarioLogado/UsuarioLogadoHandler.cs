using MeuLivroDeReceitas.Application;
using MeuLivroDeReceitas.Domain;
using Microsoft.AspNetCore.Authorization;

namespace MeuLivroDeReceitas.Api;

public class UsuarioLogadoHandler : AuthorizationHandler<UsuarioLogadoRequirement>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly TokenController _tokenController;
    private readonly IUsuarioReadOnlyRepositorio _repositorio;

    public UsuarioLogadoHandler(IHttpContextAccessor contextAccessor, TokenController tokenController, IUsuarioReadOnlyRepositorio repositorio)
    {
        _httpContextAccessor = contextAccessor;
        _tokenController = tokenController;
        _repositorio = repositorio;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, UsuarioLogadoRequirement requirement)
    {
        try
        {
            var authorization = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();

            if (string.IsNullOrWhiteSpace(authorization))
            {
                context.Fail();
                return;
            }

            var token = authorization["Bearer".Length..].Trim();
            var emailUsuario = _tokenController.RecuperarEmail(token);
            var usuario = await _repositorio.RecuperarPorEmail(emailUsuario);

            if (usuario != null)
            {
                context.Fail();
                return;
            }
            else
                context.Succeed(requirement);
        }
        catch
        {
            context.Fail();
        }            
    }
}
