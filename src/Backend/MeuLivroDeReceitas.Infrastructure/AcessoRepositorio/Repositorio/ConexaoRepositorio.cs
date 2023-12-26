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

    public async Task Registrar(Conexoes conexao)
    {
        await _context.Conexoes.AddAsync(conexao);
    }
}
