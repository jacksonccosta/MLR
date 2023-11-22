using MeuLivroDeReceitas.Api.Filtros;
using MeuLivroDeReceitas.Application;
using MeuLivroDeReceitas.Comunicacao;
using Microsoft.AspNetCore.Mvc;

namespace MeuLivroDeReceitas.Api.Controllers;

[ServiceFilter(typeof(UsuarioAutenticadoAttribute))]
public class ReceitaController : MlrController
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseReceitaJson), StatusCodes.Status201Created)]
    public async Task<IActionResult> Registrar(
        [FromServices] IRegistrarReceitaUseCase useCase, 
        [FromBody] RequestRegistrarReceitaJson request)
    {
        var response = await useCase.Executar(request);

        return Created(string.Empty, response);
    }
}

