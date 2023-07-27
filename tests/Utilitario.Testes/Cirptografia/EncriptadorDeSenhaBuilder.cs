using MeuLivroDeReceitas.Application;

namespace Utilitario.Testes;

public class EncriptadorDeSenhaBuilder
{
    public static Encriptador Instancia()
    {
        return new Encriptador("ABCDE321");
    }
}
