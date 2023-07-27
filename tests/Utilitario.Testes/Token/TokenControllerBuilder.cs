using MeuLivroDeReceitas.Application;

namespace Utilitario.Testes.Token;

public class TokenControllerBuilder
{
    public static TokenController Instancia()
    {
        return new TokenController(1000, "OVRTczBOeHg1KjhkR3hZWDJk");
    }
}
