using MeuLivroDeReceitas.Application;

namespace Utilitario.Testes.Token;

public class TokenControllerBuilder
{
    public static TokenController Instancia()
    {
        return new TokenController(1000, "dW01WCVqJWY5MDBqbFNRVEZaSm96ckxHJSpLZFVwZiRmSVIkUXlyb2xVTGshdGYzVlg=");
    }
}
