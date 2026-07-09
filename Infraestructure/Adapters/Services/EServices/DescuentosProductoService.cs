using Domain.Entities;
using Domain.Ports.Services.EServices;
using Domain.Ports.Repositories;

namespace Infraestructure.Adapters.Services.EServices;

public class DescuentosProductoService :
    IDescuentosProductoService
{
    private readonly IUnitOfWork _unitOfWork;

    public DescuentosProductoService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    public Task<DescuentosProducto> GetAllAsync(CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<DescuentosProducto> GetByIdAsync(int id, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task CreateAsync(DescuentosProducto entity, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(DescuentosProducto entity, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(int id, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}
