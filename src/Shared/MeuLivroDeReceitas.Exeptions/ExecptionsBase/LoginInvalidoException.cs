namespace MeuLivroDeReceitas.Exeptions;

public class LoginInvalidoException : MeuLivroDeReceitasException
{
    public LoginInvalidoException() : base(ResourceMensagensDeErro.LOGIN_INVALIDO)
    {
        
    }
}
