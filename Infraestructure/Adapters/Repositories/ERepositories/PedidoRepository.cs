using Domain.DTO_s.Pedido;
using Domain.Entities;
using Domain.Ports.Repositories.ERepositories;
using Infraestructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Adapters.Repositories.ERepositories;

public class PedidoRepository :
    GRepositories<Pedido>,
    IPedidoRepository
{
    public PedidoRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<OrdersQuantityClientDto?> GetClientwithMostOrders(CancellationToken ct)
    {
        return await _dbSet.AsNoTracking()
            .Select(x => new OrdersQuantityClientDto
            {
                nombre = x.cliente.nombre,
                farmacia = x.cliente.farmacia,
                qunatity = x.detalle_pedidos.Count
            })
            .OrderByDescending(o => o.qunatity)
            .FirstOrDefaultAsync(ct);
    }

    public async Task<decimal> GetTotalRevenue(CancellationToken ct)
    {
        return await _dbSet.AsNoTracking()
            .SumAsync(s => s.total);
    }
}
