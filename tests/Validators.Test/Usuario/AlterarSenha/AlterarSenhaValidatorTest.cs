using FluentAssertions;
using MeuLivroDeReceitas.Application;
using MeuLivroDeReceitas.Exeptions;
using Utilitario.Testes.Request;
using Xunit;

namespace Validators.Test.Usuario.AlterarSenha;

public class AlterarSenhaValidatorTest
{
    [Fact]
    public void Validar_Sucessoo()
    {
        var validator = new AlterarSenhaValidator();

        var request = RequestAlterarSenhaUsuarioBuilder.Contruir();

        var resultado = validator.Validate(request);

        resultado.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validar_Erro_Senha_Vazio()
    {
        var validator = new AlterarSenhaValidator();

        var request = RequestAlterarSenhaUsuarioBuilder.Contruir();
        request.NovaSenha = string.Empty;

        var resultado = validator.Validate(request);

        resultado.IsValid.Should().BeFalse();
        resultado.Errors.Should().ContainSingle().And.Contain(erro => erro.ErrorMessage.Equals(ResourceMensagensDeErro.SENHA_USUARIO_EMBRANCO));
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public void Validar_Erro_Senha_Invalido(int tamanhoSenha)
    {
        var validator = new AlterarSenhaValidator();

        var request = RequestAlterarSenhaUsuarioBuilder.Contruir(tamanhoSenha);

        var resultado = validator.Validate(request);

        resultado.IsValid.Should().BeFalse();
        resultado.Errors.Should().ContainSingle().And.Contain(erro => erro.ErrorMessage.Equals(ResourceMensagensDeErro.SENHA_USUARIO_MINIMO_SEIS_CARACTERES));
    }
}
