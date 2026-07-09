namespace Domain.Ports.Services;

public interface IGService<T> 
    where T: class
{
    Task<T> GetAllAsync(CancellationToken ct);
    Task<T> GetByIdAsync(int id, CancellationToken ct);
    Task CreateAsync(T entity, CancellationToken ct);
    Task<bool> UpdateAsync(T entity, CancellationToken ct);
    Task<bool> DeleteAsync(int id, CancellationToken ct);
}