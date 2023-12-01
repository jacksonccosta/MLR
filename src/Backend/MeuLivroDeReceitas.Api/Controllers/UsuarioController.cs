using MeuLivroDeReceitas.Api.Filtros;
using MeuLivroDeReceitas.Application;
using MeuLivroDeReceitas.Comunicacao;
using Microsoft.AspNetCore.Mvc;

namespace MeuLivroDeReceitas.Api.Controllers;

public class UsuarioController : MlrController
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseUsuarioRegistradoJson), StatusCodes.Status201Created)]
    public async Task<IActionResult> RegistrarUsuario([FromServices] IRegistrarUsuarioUseCase useCase, [FromBody] RequestRegistrarUsuarioJson request)
    {
        var result = await useCase.Executar(request);

        return Created(string.Empty, result);
    }

    [HttpPut]
    [Route("alterar-senha")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ServiceFilter(typeof(UsuarioAutenticadoAttribute))]
    public async Task<IActionResult> AlterarSenha(
        [FromServices] IAlterarSenhaUseCase useCase, 
        [FromBody] RequestAlterarSenhaJson request)
    {
        await useCase.Executar(request);

        return NoContent();
    }

    [HttpGet]
    [ProducesResponseType(typeof(ResponsePerfilUsuarioJson), StatusCodes.Status200OK)]
    [ServiceFilter(typeof(UsuarioAutenticadoAttribute))]
    public async Task<IActionResult> RecuperarPerfil(
        [FromServices] IRecuperarPerfilUsuarioUseCase useCase)
    {
        var resultado = await useCase.Executar();
        return Ok(resultado);
    }
}