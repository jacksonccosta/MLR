using MeuLivroDeReceitas.Domain;

namespace MeuLivroDeReceitas.Infrastructure
{
    public class ReceitaRepositorio : IReceitaWriteOnlyRepositorio
    {
        private readonly MeuLivroDeReceitaContext _contexto;

        public ReceitaRepositorio(MeuLivroDeReceitaContext contexto)
        {
            _contexto = contexto;
        }

        public async Task Registrar(Receita receita)
        {
            await _contexto.Receitas.AddAsync(receita);
        }
    }
}
