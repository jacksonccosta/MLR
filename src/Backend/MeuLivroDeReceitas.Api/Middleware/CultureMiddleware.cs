using System.Globalization;

namespace MeuLivroDeReceitas.Api.Middleware;

public class CultureMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IList<string> _idomasSuportados = new List<string>
    {
        "pt",
        "en"
    };

    public CultureMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var culture = new CultureInfo("pt");

        if(context.Request.Headers.ContainsKey("Accept-Language"))
        {
            var linguagem = context.Request.Headers["Accept-Language"].ToString();

            if (_idomasSuportados.Any(c => c.Equals(linguagem)))
                culture = new CultureInfo(linguagem);  
        }

        CultureInfo.CurrentCulture = culture;
        CultureInfo.CurrentUICulture = culture;

        await _next(context);
    }
}
