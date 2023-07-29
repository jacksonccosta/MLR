using FluentAssertions;
using MeuLivroDeReceitas.Application;
using MeuLivroDeReceitas.Comunicacao;
using MeuLivroDeReceitas.Domain;
using MeuLivroDeReceitas.Exeptions;
using Utilitario.Testes;
using Utilitario.Testes.Repositorios;
using Xunit;

namespace UserCases.Test;

public class AlterarSenhaUseCaseTest
{
    [Fact]
    public async Task Validar_Sucvesso()
    {
        (var usuario, var senha) = UsuarioBuilder.Contruir();
        var useCase = CriarUseCase(usuario);

        var requisicao = RequestAlterarSenhaUsuarioBuilder.Contruir();
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
        (var usuario, var senha) = UsuarioBuilder.Contruir();
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

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public async Task Validar_Erro_SenhaAtual_Invalida(int tamanhoSenha)
    {
        (var usuario, var senha) = UsuarioBuilder.Contruir();
        var useCase = CriarUseCase(usuario);

        var requisicao = RequestAlterarSenhaUsuarioBuilder.Contruir(tamanhoSenha);
        requisicao.SenhaAtual = senha;

        Func<Task> acao = async () =>
        {
            await useCase.Executar(requisicao);
        };

        await acao.Should().ThrowAsync<ErrosDeValidacaoException>()
            .Where(ex => ex.MensagensDeErro.Count == 1 && ex.MensagensDeErro.Contains(ResourceMensagensDeErro.SENHA_USUARIO_MINIMO_SEIS_CARACTERES)); ;
    }

    private AlterarSenhaUseCase CriarUseCase(Usuario usuario)
    {
        var encriptador = EncriptadorDeSenhaBuilder.Instancia();
        var unidadeDeTrabalho = UnidadeDeTrabalhoBuilder.Instancia().Construir();
        var repositorio = UsuarioUpdateOnlyRepositorioBuilder.Instancia().RecuperarPorId(usuario).Construir();
        var usuarioLogado = UsuarioLogadoBuilder.Instancia().RecuperarUsuario(usuario).Construir();

        return new AlterarSenhaUseCase(repositorio, usuarioLogado, encriptador, unidadeDeTrabalho);
    }
}
