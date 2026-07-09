using Domain.Entities;
using Domain.Ports.Services.EServices;
using Domain.Ports.Repositories;

namespace Infraestructure.Adapters.Services.EServices;

public class PedidoService :
    IPedidoService
{
    private readonly IUnitOfWork _unitOfWork;

    public PedidoService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    public Task<Pedido> GetAllAsync(CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<Pedido> GetByIdAsync(int id, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task CreateAsync(Pedido entity, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(Pedido entity, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(int id, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}
