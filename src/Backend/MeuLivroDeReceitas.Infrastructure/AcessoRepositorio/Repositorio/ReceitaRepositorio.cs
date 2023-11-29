using MeuLivroDeReceitas.Domain;
using System.Data.Entity;

namespace MeuLivroDeReceitas.Infrastructure
{
    public class ReceitaRepositorio : IReceitaWriteOnlyRepositorio, IReceitaReadOnlyRepositorio
    {
        private readonly MeuLivroDeReceitaContext _contexto;

        public ReceitaRepositorio(MeuLivroDeReceitaContext contexto)
        {
            _contexto = contexto;
        }

        public async Task<List<Receita>> RecuperaReceitasUsuario(long usuarioId)
        {
            return await _contexto.Receitas
                .Include(r => r.Ingredientes)
                .Where(r => r.UsuarioId == usuarioId).ToListAsync();
        }

        public async Task Registrar(Receita receita)
        {
            await _contexto.Receitas.AddAsync(receita);
        }
    }
}
