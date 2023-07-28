using MeuLivroDeReceitas.Application;
using MeuLivroDeReceitas.Comunicacao;
using Microsoft.AspNetCore.Mvc;

namespace MeuLivroDeReceitas.Api.Controllers;

public class LoginController : MLRController
{
    [HttpPost]
    [ProducesResponseType(typeof(RequestLoginJson), StatusCodes.Status200OK)]
    public async Task<IActionResult> Login(
        [FromServices] ILoginUseCase useCase, 
        [FromBody] RequestLoginJson request)
    {
        var response = await useCase.Executar(request);

        return Ok(response);
    }
}