using Microsoft.EntityFrameworkCore;

namespace Database.Repositories.InnerDependencies;

public abstract class DisposableRepository<T> : IDisposable where T : DbContext
{
    private bool disposed = false;
    protected T _context;

    public DisposableRepository(T context) { this._context = context; }

    protected virtual void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
        this.disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
