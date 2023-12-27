using MeuLivroDeReceitas.Domain;
using System.Data.Entity;

namespace MeuLivroDeReceitas.Infrastructure;

public class ConexaoRepositorio : IConexaoReadOnlyRepositorio, IConexaoWriteOnlyRepositorio
{
    private readonly MeuLivroDeReceitaContext _context;

    public ConexaoRepositorio(MeuLivroDeReceitaContext context)
    {
        _context = context;
    }
    public async Task<bool> ExisteConexao(long codeUserOne, long codeUserTwo)
    {
        return await _context.Conexoes.AnyAsync(c => c.UsuarioId == codeUserOne && c.ConexaoUsuarioId == codeUserTwo);
    }

    public async Task<IList<Usuario>> RecuperarDoUsuario(long usuarioId)
    {
        return await _context.Conexoes.AsNoTracking()
            .Include(c => c.ConectadoComUsuario)
            .Where(c => c.UsuarioId == usuarioId)
            .Select(c => c.ConectadoComUsuario)
            .ToListAsync();
    }

    public async Task Registrar(Conexoes conexao)
    {
        await _context.Conexoes.AddAsync(conexao);
    }

    public async Task RemoverConexao(long usuarioId, long usuarioIdParaRemover)
    {
        var conexoes = await _context.Conexoes
            .Where(c => (c.UsuarioId == usuarioId && c.ConexaoUsuarioId == usuarioIdParaRemover)
                   || (c.UsuarioId == usuarioIdParaRemover && c.ConexaoUsuarioId == usuarioId)).ToListAsync();
       
        _context.Conexoes.RemoveRange(conexoes);
    }
}
