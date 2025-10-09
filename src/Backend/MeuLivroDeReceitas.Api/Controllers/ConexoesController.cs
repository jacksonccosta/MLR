using MeuLivroDeReceitas.Application;
using MeuLivroDeReceitas.Comunicacao;
using Microsoft.AspNetCore.Mvc;

namespace MeuLivroDeReceitas.Api.Controllers;

[ServiceFilter(typeof(UsuarioAutenticadoAttribute))]
public class ConexoesController : MlrController
{
    [HttpGet]
    [ProducesResponseType(typeof(IList<ResponseUsuarioConectadoJson>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> RecuperaConexoes(
        [FromServices] IRecuperarTodasAsConexoesUseCase useCase)
    {
        var resultado = await useCase.Executar();

        if (resultado.Any())
            return Ok(resultado);

        return NoContent();
    }

    [HttpDelete]
    [Route("{id:hashids}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> RemoverConexao(
        [FromServices] IRemoverConexaoUseCase useCase,
        [FromRoute][ModelBinder(typeof(HashidsModelBinder))] long id)
    {
        await useCase.Executar(id);

        return NoContent();
    }
}
