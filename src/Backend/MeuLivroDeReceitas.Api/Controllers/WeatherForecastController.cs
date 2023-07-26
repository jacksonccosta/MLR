using MeuLivroDeReceitas.Exeptions;
using Microsoft.AspNetCore.Mvc;

namespace MeuLivroDeReceitas.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    [HttpGet(Name = "GetWeatherForecast")]
    public IActionResult Get()
    {
        var msg = ResourceMensagensDeErro.NOME_USUARIO_EMBRANCO;

        return Ok();
    }
}