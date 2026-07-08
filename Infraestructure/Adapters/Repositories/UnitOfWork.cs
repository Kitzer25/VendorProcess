using Domain.Ports.Repositories;
using Domain.Ports.Repositories.ERepositories;
using Infraestructure.Adapters.Repositories.ERepositories;
using Infraestructure.Context;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infraestructure.Adapters.Repositories;

public class UnitOfWork: IUnitOfWork
{
    private readonly AppDbContext _context;
    private readonly IDictionary<Type, object> _repositories;
    private IDbContextTransaction? _transaction;
    
    
    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        _repositories = new Dictionary<Type, object>();
    }
    
    //Repositories 
    public IDescuentosProductoRepository DiscountProductRepo =>
        GetRepository(() => new DescuentosProductoRepository(_context));
    public IDetallePedidoRepository DetailOrderRepo => 
        GetRepository(() =>  new DetallePedidoRepository(_context));
    public ILaboratorioRepository LaboratoryRepo => 
        GetRepository(() => new LaboratorioRepository(_context));
    public IPedidoRepository OrderRepo => 
        GetRepository(() => new PedidoRepository(_context));
    public IProductoRepository ProductRepo => 
        GetRepository(() => new ProductoRepository(_context));
    public IUsuarioRepository UserRepo => 
        GetRepository(() => new UsuarioRepository(_context));


    private TRepository GetRepository<TRepository>
        (Func<TRepository> factory)
        where TRepository : class
    {
        var type = typeof(TRepository);
        
        if(_repositories.TryGetValue(type, out var repository))
            return (TRepository)repository;

        var instance = factory();
        
        _repositories[type] = instance;
        return instance;
    }
    

    public IGRepositories<T> Repositories<T>() where T : class
    {
        var type = typeof(T);

        if (_repositories.TryGetValue(type, out var repositories))
        {
            return (IGRepositories<T>)repositories;
        }

        var repositoryInstance = new GRepositories<T>(_context);

        _repositories.Add(type, repositoryInstance);

        return repositoryInstance;
    }
    

    public async Task<int> SaveChangesAsync(CancellationToken ct)
    {
        return await _context.SaveChangesAsync(ct);
    }
    

    public async Task BeginTransactionAsync(CancellationToken ct)
    {
        if (_transaction != null)
        {
            return;
        }

        _transaction = await _context.Database.BeginTransactionAsync(ct);
    }
    

    public async Task CommitTransactionAsync(CancellationToken ct)
    {
        if (_transaction == null)
        {
            return;
        }

        await _transaction.CommitAsync(ct);
        await _transaction.DisposeAsync();
        _transaction = null;
    }
    

    public async Task RollbackTransactionAsync(CancellationToken ct)
    {
        if (_transaction == null)
        {
            return;
        }

        await _transaction.RollbackAsync(ct);
        await _transaction.DisposeAsync();
        _transaction = null;
    }
    

    public void Dispose()
    {
        _transaction?.Dispose();
        _context.Dispose();
    }
}