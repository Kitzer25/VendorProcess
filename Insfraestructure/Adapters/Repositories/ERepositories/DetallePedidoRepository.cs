using Domain.Entities;
using Domain.Ports.Repositories.ERepositories;
using Insfraestructure.Context;

namespace Insfraestructure.Adapters.Repositories.ERepositories;

public class DetallePedidoRepository :
    GRepositories<DetallePedido>,
    IDetallePedidoRepository
{
    public DetallePedidoRepository(AppDbContext context) : base(context)
    {
    }
}
