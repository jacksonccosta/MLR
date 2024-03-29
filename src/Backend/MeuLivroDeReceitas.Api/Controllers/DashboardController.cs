﻿using MeuLivroDeReceitas.Api.Controllers;
using MeuLivroDeReceitas.Application;
using MeuLivroDeReceitas.Comunicacao;
using Microsoft.AspNetCore.Mvc;

namespace MeuLivroDeReceitas.Api;

[ServiceFilter(typeof(UsuarioAutenticadoAttribute))]
public class DashboardController : MlrController
{
    [HttpPut]
    [ProducesResponseType(typeof(ResponseDashboardJson),StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    
    public async Task<IActionResult> RecuperaDashboard(
        [FromServices] IDashboardUseCase useCase,
        [FromBody] RequestDashboardJson request)
    {
        var resultado = await useCase.Executar(request);

        if (resultado.Receitas.Any())
            return Ok(resultado);

        return NoContent();
    }
}
