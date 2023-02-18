using Microsoft.EntityFrameworkCore;

namespace Database.Repositories.InnerDependencies;

public abstract class DisposableRepository<T> : IDisposable where T : DbContext
{
    private bool _disposed = false;
    protected T _context;

    public DisposableRepository(T context) 
    {
        _context = context; 
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
        {
            _context.Dispose();
        }
        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public Task SaveAsync() => _context.SaveChangesAsync();
}
