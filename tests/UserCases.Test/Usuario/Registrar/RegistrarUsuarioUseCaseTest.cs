﻿using FluentAssertions;
using MeuLivroDeReceitas.Application;
using MeuLivroDeReceitas.Exeptions;
using Utilitario.Testes;
using Utilitario.Testes.Repositorios;
using Utilitario.Testes.Token;
using Xunit;

namespace UseCases.Test;

public class RegistrarUsuarioUseCaseTest
{
    [Fact]
    public async Task Validar_Sucesso()
    {
        var request = RequestRegistrarUsuarioBuilder.Construir();

        var useCase = CriarUseCase();

        var resposta = await useCase.Executar(request);

        resposta.Should().NotBeNull();
        resposta.Token.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task Validar_Erro_Email_Duplicado()
    {
        var request = RequestRegistrarUsuarioBuilder.Construir();

        var useCase = CriarUseCase(request.Email);

        Func<Task> acao = async () => { await useCase.Executar(request); };

        await acao.Should().ThrowAsync<ErrosDeValidacaoException>()
             .Where(e => e.MensagensDeErro.Count == 1 && e.MensagensDeErro.Contains(ResourceMensagensDeErro.EMAIL_JA_REGISTRADO));
    }

    [Fact]
    public async Task Validar_Erro_Email_Vazio()
    {
        var request = RequestRegistrarUsuarioBuilder.Construir();
        request.Email = string.Empty;

        var useCase = CriarUseCase();

        Func<Task> acao = async () => { await useCase.Executar(request); };

        await acao.Should().ThrowAsync<ErrosDeValidacaoException>()
             .Where(e => e.MensagensDeErro.Count == 1 && e.MensagensDeErro.Contains(ResourceMensagensDeErro.EMAIL_USUARIO_EMBRANCO));
    }

    private static RegistrarUsuarioUseCase CriarUseCase(string email = "")
    {
        var mapper = MapperBuilder.Instancia();
        var repositorio = UsuarioWriteOnlyRepositorioBuilder.Instancia().Construir();
        var unidadeDeTrabalho = UnidadeDeTrabalhoBuilder.Instancia().Construir();
        var encriptador = EncriptadorDeSenhaBuilder.Instancia();
        var token = TokenControllerBuilder.Instancia();
        var repositorioReadOnly = UsuarioReadOnlyRepositorioBuilder.Instancia().ExisteUsuarioComEmail(email).Construir();

        return new RegistrarUsuarioUseCase(repositorio, mapper, unidadeDeTrabalho, encriptador, token, repositorioReadOnly);
    }
}
