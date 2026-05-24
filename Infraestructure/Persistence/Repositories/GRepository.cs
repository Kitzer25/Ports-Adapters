using Domain.Ports;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Persistence.Repositories;

public class GRepository<T> : IGRepository<T>
    where T : class
{
    private readonly AppDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public GRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }


    public async Task<IEnumerable<T>> GetAll(CancellationToken ct)
    {
        return await _dbSet.AsNoTracking().ToListAsync(ct);
    }

    public async Task<T?> GetById(Guid id, CancellationToken ct)
    {
        return await _dbSet.FindAsync(id, ct);
    }

    public async Task Add(T entity, CancellationToken ct)
    {
        await _dbSet.AddAsync(entity, ct);
        await _context.SaveChangesAsync(ct);
    }

    public async Task Update(Guid id, T entity, CancellationToken ct)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync(ct);
    }

    public async Task Delete(T entity, CancellationToken ct)
    {
        _dbSet.Remove(entity);
        await _context.SaveChangesAsync(ct);
    }
}