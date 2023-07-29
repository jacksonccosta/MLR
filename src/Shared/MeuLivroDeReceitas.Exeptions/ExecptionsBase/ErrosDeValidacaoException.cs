using System.Runtime.Serialization;

namespace MeuLivroDeReceitas.Exeptions;

[Serializable]
public class ErrosDeValidacaoException : MeuLivroDeReceitasException
{
    public List<string> MensagensDeErro { get; set; }

    public ErrosDeValidacaoException(List<string> mensagensDeErro) : base(string.Empty)
    {
        MensagensDeErro = mensagensDeErro;
    }

    protected ErrosDeValidacaoException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}
