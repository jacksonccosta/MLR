using MeuLivroDeReceitas.Application;
using MeuLivroDeReceitas.Comunicacao;
using Microsoft.AspNetCore.Mvc;

namespace MeuLivroDeReceitas.Api.Controllers;

public class UsuarioController : MLRController
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseUsuarioRegistradoJson), StatusCodes.Status201Created)]
    public async Task<IActionResult> RegistrarUsuario([FromServices] IRegistrarUsuarioUseCase useCase, [FromBody] RequestRegistrarUsuarioJson request)
    {
        var result = await useCase.Executar(request);

        return Created(string.Empty, result);
    }
}