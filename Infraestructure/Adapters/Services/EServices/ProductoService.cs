using Domain.Entities;
using Domain.Ports.Services.EServices;
using Domain.Ports.Repositories;

namespace Infraestructure.Adapters.Services.EServices;

public class ProductoService :
    IProductoService
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductoService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    public Task<Producto> GetAllAsync(CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<Producto> GetByIdAsync(int id, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task CreateAsync(Producto entity, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(Producto entity, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(int id, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}
