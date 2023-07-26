using MeuLivroDeReceitas.Domain;

namespace MeuLivroDeReceitas.Infrastructure;

public sealed class UnidadeDeTrabalho : IDisposable, IUnidadeDeTrabalho
{
    private readonly MeuLivroDeReceitaContext _context;
    private bool _disposed;

    public UnidadeDeTrabalho(MeuLivroDeReceitaContext context)
    {
        _context = context;
    }

    public async Task Commit()
    {
        await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        Dispose(true);
    }

    public void Dispose(bool disposed)
    {
        if(!_disposed && disposed)
            _context.Dispose();
        
        _disposed = true;
    }
}
