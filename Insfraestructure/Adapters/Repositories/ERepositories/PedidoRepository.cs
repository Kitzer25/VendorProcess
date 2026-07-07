using Domain.Entities;
using Domain.Ports.Repositories.ERepositories;
using Insfraestructure.Context;

namespace Insfraestructure.Adapters.Repositories.ERepositories;

public class PedidoRepository :
    GRepositories<Pedido>,
    IPedidoRepository
{
    public PedidoRepository(AppDbContext context) : base(context)
    {
    }
}
