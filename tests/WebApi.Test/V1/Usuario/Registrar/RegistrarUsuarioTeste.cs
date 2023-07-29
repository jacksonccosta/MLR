using FluentAssertions;
using MeuLivroDeReceitas.Exeptions;
using System.Net;
using System.Text.Json;
using Utilitario.Testes;
using Xunit;

namespace WebApi.Test.V1;

public class RegistrarUsuarioteste : ControllerBase
{
    private const string METODO = "usuario";
    public RegistrarUsuarioteste(MeuLivroDeReceitaWebApplicationFactory<Program> factory) 
        : base(factory)
    {

    }

    [Fact]
    public async Task Validar_Sucesso()
    {
        var request = RequestRegistrarUsuarioBuilder.Construir();

        var resposta = await PostRequest(METODO, request);

        resposta.StatusCode.Should().Be(HttpStatusCode.Created);

        await using var responseBody = await resposta.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        responseData.RootElement.GetProperty("token").GetString().Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task Validar_Erro_Nome_Vazio()
    {
        var request = RequestRegistrarUsuarioBuilder.Construir();
        request.Nome= string.Empty;

        var resposta = await PostRequest(METODO, request);

        resposta.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        await using var responseBody = await resposta.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        var erro = responseData.RootElement.GetProperty("mensagens").EnumerateArray();
        erro.Should().ContainSingle().And.Contain(c => c.GetString().Equals(ResourceMensagensDeErro.NOME_USUARIO_EMBRANCO));
    }
}
