using MeuLivroDeReceitas.Domain;
using Microsoft.EntityFrameworkCore;

namespace MeuLivroDeReceitas.Infrastructure
{
    public class ReceitaRepositorio : IReceitaWriteOnlyRepositorio, IReceitaReadOnlyRepositorio, IReceitaUpdateOnlyRepositorio
    {
        private readonly MeuLivroDeReceitaContext _contexto;

        public ReceitaRepositorio(MeuLivroDeReceitaContext contexto)
        {
            _contexto = contexto;
        }

        async Task<Receita> IReceitaReadOnlyRepositorio.RecuperaReceitasPorId(long receitaId)
        {
            return await _contexto.Receitas.AsNoTracking()
                .Include(r => r.Ingredientes)
                .FirstOrDefaultAsync(r => r.Id == receitaId);
        }

        async Task<Receita> IReceitaUpdateOnlyRepositorio.RecuperaReceitasPorId(long receitaId)
        {
            return await _contexto.Receitas
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

        public void Update(Receita receita)
        {
            _contexto.Receitas.Update(receita);
        }

        public async Task Deletar(long receitaId)
        {
            var receita = await _contexto.Receitas.FirstOrDefaultAsync(r => r.Id == receitaId);
            _contexto.Receitas.Remove(receita);
        }
    }
}
