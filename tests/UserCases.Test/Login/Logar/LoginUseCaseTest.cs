using FluentAssertions;
using MeuLivroDeReceitas.Application;
using MeuLivroDeReceitas.Domain;
using MeuLivroDeReceitas.Exeptions;
using Utilitario.Testes;
using Utilitario.Testes.Token;
using Xunit;

namespace UserCases.Test;

public class LoginUseCaseTest
{
    [Fact]
    public async Task Validar_Sucesso()
    {
        (var usuario, var senha) = UsuarioBuilder.Construir();

        var useCase = CriarUseCase(usuario);
        var response = await useCase.Executar(new MeuLivroDeReceitas.Comunicacao.RequestLoginJson
        {
            Email = usuario.Email,
            Senha = senha
        });

        response.Should().NotBeNull();
        response.Nome.Should().Be(usuario.Nome);
        response.Token.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task Validar_Erro_Senha_Invalida()
    {
        (var usuario, var senha) = UsuarioBuilder.Construir();

        var useCase = CriarUseCase(usuario);

        Func<Task> acao = async () =>
        {
            await useCase.Executar(new MeuLivroDeReceitas.Comunicacao.RequestLoginJson
            {
                Email = usuario.Email,
                Senha = "senhaInvalida"
            });
        };

        await acao.Should().ThrowAsync<LoginInvalidoException>()
            .Where(execption => execption.Message.Equals(ResourceMensagensDeErro.LOGIN_INVALIDO));
    }

    [Fact]
    public async Task Validar_Erro_Email_Invalida()
    {
        (var usuario, var senha) = UsuarioBuilder.Construir();

        var useCase = CriarUseCase(usuario);

        Func<Task> acao = async () =>
        {
            await useCase.Executar(new MeuLivroDeReceitas.Comunicacao.RequestLoginJson
            {
                Email = "email@invalido.com",
                Senha = senha
            });
        };

        await acao.Should().ThrowAsync<LoginInvalidoException>()
            .Where(execption => execption.Message.Equals(ResourceMensagensDeErro.LOGIN_INVALIDO));
    }

    [Fact]
    public async Task Validar_Erro_Login_Invalida()
    {
        (var usuario, var senha) = UsuarioBuilder.Construir();

        var useCase = CriarUseCase(usuario);

        Func<Task> acao = async () =>
        {
            await useCase.Executar(new MeuLivroDeReceitas.Comunicacao.RequestLoginJson
            {
                Email = "email@invalido.com",
                Senha = "senhaInvalida"
            });
        };

        await acao.Should().ThrowAsync<LoginInvalidoException>()
            .Where(execption => execption.Message.Equals(ResourceMensagensDeErro.LOGIN_INVALIDO));
    }

    private LoginUseCase CriarUseCase(Usuario usuario)
    {
        var repositorioReadOnly = UsuarioReadOnlyRepositorioBuilder.Instancia().RecuperarLogin(usuario).Construir();
        var encriptador = EncriptadorDeSenhaBuilder.Instancia();
        var token = TokenControllerBuilder.Instancia();

        return new LoginUseCase(repositorioReadOnly, encriptador, token);
    }
}
