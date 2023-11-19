namespace MeuLivroDeReceitas.Domain
{
    public interface IReceitaWriteOnlyRepositorio
    {
        Task Registrar(Receita receita);
    }
}
