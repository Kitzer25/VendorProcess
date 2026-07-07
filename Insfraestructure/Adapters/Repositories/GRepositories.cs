using System.Reflection.Metadata.Ecma335;
using Domain.Ports.Repositories;
using Insfraestructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Insfraestructure.Adapters.Repositories;

public class GRepositories<T> : IGRepositories<T> 
    where T: class
{
    private readonly AppDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public GRepositories(AppDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAll(int id, CancellationToken ct)
    {
        return await _dbSet.AsNoTracking()
            .ToListAsync(ct);
    }

    public async Task<T?> GetbyId(int id, CancellationToken ct)
    {
        return await _dbSet.FindAsync(id, ct);
    }

    public async Task Add(T entity, CancellationToken ct)
    {
        await _dbSet.AddAsync(entity, ct);
    }

    public Task Update(T entity, CancellationToken ct)
    {
        _dbSet.Update(entity);
        return Task.CompletedTask;
    }

    public Task<bool> Delete(T entity, CancellationToken ct)
    {
        _dbSet.Remove(entity);
        return Task.FromResult(true);
    }
}