namespace Domain.Ports;

public interface IGRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAll(CancellationToken ct);
    Task<T?> GetById(Guid id, CancellationToken ct);
    Task Add(T entity, CancellationToken ct);
    Task Update(Guid id, T entity, CancellationToken ct);
    Task Delete(T entity, CancellationToken ct);
}