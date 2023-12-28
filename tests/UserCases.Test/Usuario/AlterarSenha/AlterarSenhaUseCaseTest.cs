using FluentAssertions;
using MeuLivroDeReceitas.Application;
using MeuLivroDeReceitas.Comunicacao;
using MeuLivroDeReceitas.Domain;
using MeuLivroDeReceitas.Exeptions;
using Utilitario.Testes;
using Utilitario.Testes.Repositorios;
using Xunit;

namespace UseCases.Test;

public class AlterarSenhaUseCaseTest
{
    [Fact]
    public async Task Validar_Sucesso()
    {
        (var usuario, var senha) = UsuarioBuilder.Construir();
        var useCase = CriarUseCase(usuario);

        var requisicao = RequestAlterarSenhaUsuarioBuilder.Construir();
        requisicao.SenhaAtual = senha;

        Func<Task> acao = async () =>
        {
            await useCase.Executar(new RequestAlterarSenhaJson
            {
                SenhaAtual = senha,
                NovaSenha = "@Nova#Senha!123456"
            });
        };

        await acao.Should().NotThrowAsync();
    }

    [Fact]
    public async Task Validar_Erro_NovaSenha_Vazio()
    {
        (var usuario, var senha) = UsuarioBuilder.Construir();
        var useCase = CriarUseCase(usuario);

        Func<Task> acao = async () =>
        {
            await useCase.Executar(new RequestAlterarSenhaJson
            {
                SenhaAtual = senha,
                NovaSenha = ""
            });
        };

        await acao.Should().ThrowAsync<ErrosDeValidacaoException>()
            .Where(ex => ex.MensagensDeErro.Count == 1 && ex.MensagensDeErro.Contains(ResourceMensagensDeErro.SENHA_USUARIO_EMBRANCO)); ;
    }

    [Fact]
    public async Task Validar_Erro_Atual_Invalida()
    {
        (var usuario, var senha) = UsuarioBuilder.Construir();
        var useCase = CriarUseCase(usuario);

        var requisicao = RequestAlterarSenhaUsuarioBuilder.Construir();
        requisicao.SenhaAtual = "senhaInvalida";

        Func<Task> acao = async () =>
        {
            await useCase.Executar(requisicao);
        };

        await acao.Should().ThrowAsync<ErrosDeValidacaoException>()
            .Where(ex => ex.MensagensDeErro.Count == 1 && ex.MensagensDeErro.Contains(ResourceMensagensDeErro.SENHA_ATUAL_INVALIDA)); ;
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public async Task Validar_Erro_SenhaAtual_MinimoCaracteres(int tamanhoSenha)
    {
        (var usuario, var senha) = UsuarioBuilder.Construir();
        var useCase = CriarUseCase(usuario);

        var requisicao = RequestAlterarSenhaUsuarioBuilder.Construir(tamanhoSenha);
        requisicao.SenhaAtual = senha;

        Func<Task> acao = async () =>
        {
            await useCase.Executar(requisicao);
        };

        await acao.Should().ThrowAsync<ErrosDeValidacaoException>()
            .Where(ex => ex.MensagensDeErro.Count == 1 && ex.MensagensDeErro.Contains(ResourceMensagensDeErro.SENHA_USUARIO_MINIMO_SEIS_CARACTERES)); ;
    }

    private static AlterarSenhaUseCase CriarUseCase(Usuario usuario)
    {
        var encriptador = EncriptadorDeSenhaBuilder.Instancia();
        var unidadeDeTrabalho = UnidadeDeTrabalhoBuilder.Instancia().Construir();
        var repositorio = UsuarioUpdateOnlyRepositorioBuilder.Instancia().RecuperarPorId(usuario).Construir();
        var usuarioLogado = UsuarioLogadoBuilder.Instancia().RecuperarUsuario(usuario).Construir();

        return new AlterarSenhaUseCase(repositorio, usuarioLogado, encriptador, unidadeDeTrabalho);
    }
}
