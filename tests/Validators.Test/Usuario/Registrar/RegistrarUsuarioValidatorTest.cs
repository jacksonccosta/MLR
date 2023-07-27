using FluentAssertions;
using MeuLivroDeReceitas.Application;
using MeuLivroDeReceitas.Exeptions;
using Utilitario.Testes.Request;
using Xunit;

namespace Validators.Test.Usuario.Registrar;

public class RegistrarUsuarioValidatorTest
{
    [Fact]
    public void Validar_Sucessoo()
    {
        var validator = new RegistrarUsuarioValidator();

        var request = RequestRegistrarUsuarioBuilder.Contruir();

        var resultado = validator.Validate(request);

        resultado.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validar_Erro_Nome_Vazio()
    {
        var validator = new RegistrarUsuarioValidator();

        var request = RequestRegistrarUsuarioBuilder.Contruir();
        request.Nome = string.Empty;

        var resultado = validator.Validate(request);

        resultado.IsValid.Should().BeFalse();
        resultado.Errors.Should().ContainSingle().And.Contain(erro => erro.ErrorMessage.Equals(ResourceMensagensDeErro.NOME_USUARIO_EMBRANCO));
    }

    [Fact]
    public void Validar_Erro_Email_Vazio()
    {
        var validator = new RegistrarUsuarioValidator();

        var request = RequestRegistrarUsuarioBuilder.Contruir();
        request.Email = string.Empty;

        var resultado = validator.Validate(request);

        resultado.IsValid.Should().BeFalse();
        resultado.Errors.Should().ContainSingle().And.Contain(erro => erro.ErrorMessage.Equals(ResourceMensagensDeErro.EMAIL_USUARIO_EMBRANCO));
    }

    [Fact]
    public void Validar_Erro_Senha_Vazio()
    {
        var validator = new RegistrarUsuarioValidator();

        var request = RequestRegistrarUsuarioBuilder.Contruir();
        request.Senha = string.Empty;

        var resultado = validator.Validate(request);

        resultado.IsValid.Should().BeFalse();
        resultado.Errors.Should().ContainSingle().And.Contain(erro => erro.ErrorMessage.Equals(ResourceMensagensDeErro.SENHA_USUARIO_EMBRANCO));
    }

    [Fact]
    public void Validar_Erro_Telefone_Vazio()
    {
        var validator = new RegistrarUsuarioValidator();

        var request = RequestRegistrarUsuarioBuilder.Contruir();
        request.Telefone = string.Empty;

        var resultado = validator.Validate(request);

        resultado.IsValid.Should().BeFalse();
        resultado.Errors.Should().ContainSingle().And.Contain(erro => erro.ErrorMessage.Equals(ResourceMensagensDeErro.TELEFONE_USUARIO_EMBRANCO));
    }

    [Fact]
    public void Validar_Erro_Email_Invalido()
    {
        var validator = new RegistrarUsuarioValidator();

        var request = RequestRegistrarUsuarioBuilder.Contruir();
        request.Email = "jackson.com";

        var resultado = validator.Validate(request);

        resultado.IsValid.Should().BeFalse();
        resultado.Errors.Should().ContainSingle().And.Contain(erro => erro.ErrorMessage.Equals(ResourceMensagensDeErro.EMAIL_USUARIO_INVALIDO));
    }

    [Fact]
    public void Validar_Erro_Telefone_Invalido()
    {
        var validator = new RegistrarUsuarioValidator();

        var request = RequestRegistrarUsuarioBuilder.Contruir();
        request.Telefone = "12 3";

        var resultado = validator.Validate(request);

        resultado.IsValid.Should().BeFalse();
        resultado.Errors.Should().ContainSingle().And.Contain(erro => erro.ErrorMessage.Equals(ResourceMensagensDeErro.TELEFONE_USUARIO_INVALIDO));
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public void Validar_Erro_Senha_Invalido(int tamanhoSenha)
    {
        var validator = new RegistrarUsuarioValidator();

        var request = RequestRegistrarUsuarioBuilder.Contruir(tamanhoSenha);

        var resultado = validator.Validate(request);

        resultado.IsValid.Should().BeFalse();
        resultado.Errors.Should().ContainSingle().And.Contain(erro => erro.ErrorMessage.Equals(ResourceMensagensDeErro.SENHA_USUARIO_MINIMO_SEIS_CARACTERES));
    }
}
