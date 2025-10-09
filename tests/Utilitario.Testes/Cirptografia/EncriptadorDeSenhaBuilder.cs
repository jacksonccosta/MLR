using MeuLivroDeReceitas.Application;

namespace Utilitario.Testes;

public class EncriptadorDeSenhaBuilder
{
    public static Encriptador Instancia()
    {
        return new Encriptador("nmU@VmK&JjshF70!1D9VCe1KLTX!hk2*$!@Xm#yJ");
    }
}
