using MeuLivroDeReceitas.Application;

namespace Utilitario.Testes.Token;

public class TokenControllerBuilder
{
    public static TokenController Instancia()
    {
        return new TokenController(1000, "dW01WCVqJWY5MDBqbFNRVEZaSm96ckxHJSpLZFVwZiRmSVIkUXlyb2xVTGshdGYzVlg=");
    }
    public static TokenController TokenExpirado()
    {
        return new TokenController(0.0166667, "dW01WCVqJWY5MDBqbFNRVEZaSm96ckxHJSpLZFVwZiRmSVIkUXlyb2xVTGshdGYzVlg=");
    }
}
