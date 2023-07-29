using FluentAssertions;
using MeuLivroDeReceitas.Comunicacao;
using MeuLivroDeReceitas.Exeptions;
using System.Net;
using System.Text.Json;
using Xunit;

namespace WebApi.Test.V1;

public class LoginTest : ControllerBase
{
    private const string METODO = "login";

    private MeuLivroDeReceitas.Domain.Usuario _usuario;
    private string _senha;

    public LoginTest(MeuLivroDeReceitaWebApplicationFactory<Program> factory)
        : base(factory)
    {
        _usuario = factory.RecuperarUsuario();
        _senha = factory.RecuperarSenha();
    }

    [Fact]
    public async Task Validar_Sucesso()
    {
        var request = new RequestLoginJson
        {
            Email = _usuario.Email,
            Senha = _senha
        };

        var resposta = await PostRequest(METODO, request);

        resposta.StatusCode.Should().Be(HttpStatusCode.OK);

        await using var responseBody = await resposta.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        responseData.RootElement.GetProperty("nome").GetString().Should().NotBeNullOrWhiteSpace().And.Be(_usuario.Nome);
        responseData.RootElement.GetProperty("token").GetString().Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task Validar_Erro_Email_Invalido()
    {
        var request = new RequestLoginJson
        {
            Email = "email@invalido.com",
            Senha = _senha
        };

        var resposta = await PostRequest(METODO, request);

        resposta.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

        await using var responseBody = await resposta.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        var erro = responseData.RootElement.GetProperty("mensagens").EnumerateArray();
        erro.Should().ContainSingle().And.Contain(c => c.GetString().Equals(ResourceMensagensDeErro.LOGIN_INVALIDO));
    }

    [Fact]
    public async Task Validar_Erro_Senha_Invalido()
    {
        var request = new RequestLoginJson
        {
            Email = _usuario.Email,
            Senha = "senhaInvalida"
        };

        var resposta = await PostRequest(METODO, request);

        resposta.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

        await using var responseBody = await resposta.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        var erro = responseData.RootElement.GetProperty("mensagens").EnumerateArray();
        erro.Should().ContainSingle().And.Contain(c => c.GetString().Equals(ResourceMensagensDeErro.LOGIN_INVALIDO));
    }

    [Fact]
    public async Task Validar_Erro_Login()
    {
        var request = new RequestLoginJson
        {
            Email = "email@invalido.com",
            Senha = "senhaInvalida"
        };

        var resposta = await PostRequest(METODO, request);

        resposta.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

        await using var responseBody = await resposta.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        var erro = responseData.RootElement.GetProperty("mensagens").EnumerateArray();
        erro.Should().ContainSingle().And.Contain(c => c.GetString().Equals(ResourceMensagensDeErro.LOGIN_INVALIDO));
    }
}
