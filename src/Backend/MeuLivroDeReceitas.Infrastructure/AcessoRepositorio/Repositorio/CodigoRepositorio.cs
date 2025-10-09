using MeuLivroDeReceitas.Domain;
using Microsoft.EntityFrameworkCore;

namespace MeuLivroDeReceitas.Infrastructure;

public class CodigoRepositorio : ICodigoWriteOnlyRepositorio, ICodigoReadOnlyRepositorio
{
    private readonly MeuLivroDeReceitaContext _contexto;

    public CodigoRepositorio(MeuLivroDeReceitaContext contexto)
    {
        _contexto = contexto;
    }
    public async Task<Codigos> GetCode(string code)
    {
        return await _contexto.Codigos.AsNoTracking().FirstOrDefaultAsync(c => c.Codigo == code);
    }
    public async Task Registrar(Codigos codigo)
    {
        var codigoBancoDeDados = await _contexto.Codigos.FirstOrDefaultAsync(c => c.UsuarioId == codigo.UsuarioId);

        if(codigoBancoDeDados is not null)
        {
            codigoBancoDeDados.Codigo = codigo.Codigo;
            _contexto.Codigos.Update(codigoBancoDeDados);
        }
        else
            await _contexto.Codigos.AddAsync(codigo);
    }
    public async Task Deletar(long usuarioId)
    {
        var codigos = await _contexto.Codigos.Where(c => c.UsuarioId == usuarioId).ToListAsync();
        
        if(codigos.Any())
            _contexto.Codigos.RemoveRange(codigos);
    }
}
