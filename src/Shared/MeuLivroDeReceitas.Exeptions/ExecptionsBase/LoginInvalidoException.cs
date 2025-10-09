using System.Runtime.Serialization;

namespace MeuLivroDeReceitas.Exeptions;

[Serializable]
public class LoginInvalidoException : MeuLivroDeReceitasException
{
    public LoginInvalidoException() : base(ResourceMensagensDeErro.LOGIN_INVALIDO)
    {

    }

    protected LoginInvalidoException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}
