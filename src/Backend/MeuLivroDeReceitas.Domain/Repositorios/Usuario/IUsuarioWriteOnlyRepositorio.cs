namespace MeuLivroDeReceitas.Domain;

public interface IUsuarioWriteOnlyRepositorio
{
    Task Adicionar(Usuario usuario);
}
