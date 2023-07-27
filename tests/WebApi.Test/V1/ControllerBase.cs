using MeuLivroDeReceitas.Exeptions;
using System.Globalization;
using Newtonsoft.Json;
using Xunit;
using System.Text;

namespace WebApi.Test.V1;

public class ControllerBase : IClassFixture<MeuLivroDeReceitaWebApplicationFactory<Program>>
{
    private readonly HttpClient _httpClient;

    public ControllerBase(MeuLivroDeReceitaWebApplicationFactory<Program> factory)
    {
        _httpClient = factory.CreateClient();
        ResourceMensagensDeErro.Culture = CultureInfo.CurrentCulture;
    }

    protected async Task<HttpResponseMessage> PostRequest(string metodo, object body)
    {
        var jsonString = JsonConvert.SerializeObject(body);

        return await _httpClient.PostAsync(metodo, new StringContent(jsonString, Encoding.UTF8, "application/json"));
    }
}
