using MeuLivroDeReceitas.Exeptions;
using System.Globalization;
using Newtonsoft.Json;
using Xunit;
using System.Text;
using MeuLivroDeReceitas.Comunicacao;
using System.Text.Json;

namespace WebApi.Test.V1;

public class ControllerBase : IClassFixture<MeuLivroDeReceitaWebApplicationFactory<Program>>
{
    private readonly HttpClient _httpClient;

    public ControllerBase(MeuLivroDeReceitaWebApplicationFactory<Program> factory)
    {
        _httpClient = factory.CreateClient();
        ResourceMensagensDeErro.Culture = CultureInfo.CurrentCulture;
    }

    protected async Task<HttpResponseMessage> PostRequest(string metodo, object body, string token = "", string cultura = "")
    {
        AutorizarRequest(token);
        AlterarCulturaRequisicao(cultura);

        var jsonString = JsonConvert.SerializeObject(body);

        return await _httpClient.PostAsync(metodo, new StringContent(jsonString, Encoding.UTF8, "application/json"));
    }

    protected async Task<HttpResponseMessage> PutRequest(string metodo, object body, string token = "")
    {
        AutorizarRequest(token);

        var jsonString = JsonConvert.SerializeObject(body);

        return await _httpClient.PutAsync(metodo, new StringContent(jsonString, Encoding.UTF8, "application/json"));
    }

    protected async Task<string> Login(string email, string senha)
    {
        var request = new RequestLoginJson
        {
            Email = email,
            Senha = senha
        };

        var resposta = await PostRequest("login", request);

        await using var responseBody = await resposta.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

#pragma warning disable CS8603 // Possible null reference return.
        return responseData.RootElement.GetProperty("token").GetString();
#pragma warning restore CS8603 // Possible null reference return.
    }

    private void AutorizarRequest(string token)
    {
        if(!string.IsNullOrWhiteSpace(token))
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
    }

    private void AlterarCulturaRequisicao(string cultura)
    {
        if (!string.IsNullOrWhiteSpace(cultura))
        {
            if (_httpClient.DefaultRequestHeaders.Contains("Accept-Language"))
            {
                _httpClient.DefaultRequestHeaders.Remove("Accept-Language");
            }

            _httpClient.DefaultRequestHeaders.Add("Accept-Language", cultura);
        }
    }
}
