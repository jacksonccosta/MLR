using FluentAssertions;
using System.Net;
using Utilitario.Testes;
using Utilitario.Testes.Token;
using Xunit;

namespace WebApi.Test.V1.Usuario.AlterarSenha;

public class AlterarSenhaTokenInvalido : ControllerBase
{
    private const string METODO = "usuario/alterar-senha";

    private readonly MeuLivroDeReceitas.Domain.Usuario _usuario;
    private readonly string _senha;

    public AlterarSenhaTokenInvalido(MeuLivroDeReceitaWebApplicationFactory<Program> factory)
        : base(factory)
    {
        _usuario = factory.RecuperarUsuario();
        _senha = factory.RecuperarSenha();
    }

    [Fact]
    public async Task Validar_Erro_Token_Vazio()
    {
        var token = string.Empty;

        var request = RequestAlterarSenhaUsuarioBuilder.Construir();
        request.SenhaAtual = _senha;

        var resposta = await PutRequest(METODO, request, token);

        resposta.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task Validar_Erro_Token_Usuario_Fake()
    {
        var token = TokenControllerBuilder.Instancia().GerarToken("usuario@fake.com");

        var request = RequestAlterarSenhaUsuarioBuilder.Construir();
        request.SenhaAtual = _senha;

        var resposta = await PutRequest(METODO, request, token);

        resposta.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task Validar_Erro_Token_Expirado()
    {
        var token = TokenControllerBuilder.TokenExpirado().GerarToken(_usuario.Email);
        await Task.Delay(1000);

        var request = RequestAlterarSenhaUsuarioBuilder.Construir();
        request.SenhaAtual = _senha;

        var resposta = await PutRequest(METODO, request, token);

        resposta.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
}
