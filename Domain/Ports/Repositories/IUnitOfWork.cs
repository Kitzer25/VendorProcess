using Domain.Ports.Repositories.ERepositories;

namespace Domain.Ports.Repositories;

public interface IUnitOfWork : IDisposable
{
    public IGRepositories<T> Repositories<T>() where T : class;
    
    Task<int> SaveChangesAsync(CancellationToken ct);
    Task BeginTransactionAsync(CancellationToken ct);
    Task CommitTransactionAsync(CancellationToken ct);
    Task RollbackTransactionAsync(CancellationToken ct);
    
    //Repositories
    IDescuentosProductoRepository DiscountProductRepo { get; }
    IDetallePedidoRepository DetailOrderRepo { get; }
    ILaboratorioRepository LaboratoryRepo { get; }
    IPedidoRepository OrderRepo { get; }
    IProductoRepository ProductRepo { get; }
    IUsuarioRepository UserRepo { get; }
}