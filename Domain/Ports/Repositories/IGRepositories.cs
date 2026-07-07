namespace Domain.Ports.Repositories;

public interface IGRepositories<T>  
    where T : class
{
    Task<IEnumerable<T>> GetAll(int id, CancellationToken ct);
    Task<T?> GetbyId(int id, CancellationToken ct);
    Task Add(T entity, CancellationToken ct);
    Task Update(T entity, CancellationToken ct);
    Task<bool> Delete(T entity, CancellationToken ct);
}