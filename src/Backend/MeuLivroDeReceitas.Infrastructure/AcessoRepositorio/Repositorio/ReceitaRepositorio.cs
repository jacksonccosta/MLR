using MeuLivroDeReceitas.Domain;
using Microsoft.EntityFrameworkCore;

namespace MeuLivroDeReceitas.Infrastructure
{
    public class ReceitaRepositorio : IReceitaWriteOnlyRepositorio, IReceitaReadOnlyRepositorio
    {
        private readonly MeuLivroDeReceitaContext _contexto;

        public ReceitaRepositorio(MeuLivroDeReceitaContext contexto)
        {
            _contexto = contexto;
        }

        public async Task<Receita> RecuperaReceitasPorId(long receitaId)
        {
            return await _contexto.Receitas.AsNoTracking()
                .Include(r => r.Ingredientes)
                .FirstOrDefaultAsync(r => r.Id == receitaId);
        }

        public async Task<IList<Receita>> RecuperaReceitasUsuario(long usuarioId)
        {
            return await _contexto.Receitas.AsNoTracking()
                .Include(r => r.Ingredientes)
                .Where(r => r.UsuarioId == usuarioId).ToListAsync();
        }

        public async Task Registrar(Receita receita)
        {
            await _contexto.Receitas.AddAsync(receita);
        }
    }
}
