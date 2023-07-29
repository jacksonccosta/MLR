using FluentAssertions;
using MeuLivroDeReceitas.Exeptions;
using System.Net;
using System.Text.Json;
using Utilitario.Testes;
using Xunit;

namespace WebApi.Test.V1;

public class AlterarSenhaTest : ControllerBase
{
    private const string METODO = "usuario/alterar-senha";

    private MeuLivroDeReceitas.Domain.Usuario _usuario;
    private string _senha;

    public AlterarSenhaTest(MeuLivroDeReceitaWebApplicationFactory<Program> factory)
        : base(factory)
    {
        _usuario = factory.RecuperarUsuario();
        _senha = factory.RecuperarSenha();
    }

    [Fact]
    public async Task Validar_Sucesso()
    {
        var token = await Login(_usuario.Email, _senha);

        var request = RequestAlterarSenhaUsuarioBuilder.Construir();
        request.SenhaAtual = _senha;

        var resposta = await PutRequest(METODO, request, token);

        resposta.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task Validar_Erro_Senha_Vazio()
    {
        var token = await Login(_usuario.Email, _senha);

        var request = RequestAlterarSenhaUsuarioBuilder.Construir();
        request.SenhaAtual = _senha;
        request.NovaSenha = string.Empty;

        var resposta = await PutRequest(METODO, request, token);

        resposta.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        await using var responseBody = await resposta.Content.ReadAsStreamAsync();
        var responseData = await JsonDocument.ParseAsync(responseBody);

        var erro = responseData.RootElement.GetProperty("mensagens").EnumerateArray();
        erro.Should().ContainSingle().And.Contain(c => c.GetString().Equals(ResourceMensagensDeErro.SENHA_USUARIO_EMBRANCO));
    }
}
