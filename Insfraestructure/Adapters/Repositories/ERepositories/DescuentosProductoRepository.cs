using Domain.Entities;
using Domain.Ports.Repositories.ERepositories;
using Insfraestructure.Context;

namespace Insfraestructure.Adapters.Repositories.ERepositories;

public class DescuentosProductoRepository :
    GRepositories<DescuentosProducto>,
    IDescuentosProductoRepository
{
    public DescuentosProductoRepository(AppDbContext context) : base(context)
    {
    }
}
