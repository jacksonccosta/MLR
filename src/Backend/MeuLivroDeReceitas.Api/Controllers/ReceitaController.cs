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
        [FromBody] RequestReceitaJson request)
    {
        var response = await useCase.Executar(request);
        return Created(string.Empty, response);
    }

    [HttpGet]
    [Route("{id:hashids}")]
    [ProducesResponseType(typeof(ResponseReceitaJson), StatusCodes.Status200OK)]
    public async Task<IActionResult> RecuperarPorId(
        [FromServices] IRecuperarReceitaPorIdUseCase useCase,
        [FromRoute] [ModelBinder(typeof(HashidsModelBinder))] long id)
    {
        var response = await useCase.Executar(id);
        return Ok(response);
    }

    [HttpPut]
    [Route("{id:hashids}")]
    [ProducesResponseType(typeof(ResponseReceitaJson), StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Atualizar(
        [FromServices] IAtualizaReceitaUseCase useCase,
        [FromBody] RequestReceitaJson request,
        [FromRoute][ModelBinder(typeof(HashidsModelBinder))] long id)
    {
        await useCase.Executar(id, request);
        return NoContent();
    }

    [HttpDelete]
    [Route("{id:hashids}")]
    [ProducesResponseType(typeof(ResponseReceitaJson), StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Deletar(
        [FromServices] IDeletaReceitaUseCase useCase,
        [FromBody] RequestReceitaJson request,
        [FromRoute][ModelBinder(typeof(HashidsModelBinder))] long id)
    {
        await useCase.Executar(id);
        return NoContent();
    }
}

