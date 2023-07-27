namespace MeuLivroDeReceitas.Exeptions;

public class ErrosDeValidacaoException : MeuLivroDeReceitasException
{
    public List<string> MensagensDeErro { get; set; }

    public ErrosDeValidacaoException(List<string> mensagensDeErro) : base(string.Empty)
    {
        MensagensDeErro = mensagensDeErro;
    }
}
