using Domain.DTO_s.Pedido;
using Domain.Entities;

namespace Domain.Ports.Repositories.ERepositories;

public interface IPedidoRepository :
    IGRepositories<Pedido>
{
    Task<OrdersQuantityClientDto?> GetClientwithMostOrders(CancellationToken ct);
    Task<decimal> GetTotalRevenue(CancellationToken ct);
}
