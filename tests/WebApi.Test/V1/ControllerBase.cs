﻿using MeuLivroDeReceitas.Exeptions;
using System.Globalization;
using Newtonsoft.Json;
using Xunit;
using System.Text;
using MeuLivroDeReceitas.Comunicacao;
using MeuLivroDeReceitas.Domain;
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

    protected async Task<HttpResponseMessage> PostRequest(string metodo, object body)
    {
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

        return responseData.RootElement.GetProperty("token").GetString();
    }

    private void AutorizarRequest(string token)
    {
        if(!string.IsNullOrWhiteSpace(token))
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
    }
}
