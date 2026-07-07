using Domain.Entities;
using Domain.Ports.Repositories.ERepositories;
using Insfraestructure.Context;

namespace Insfraestructure.Adapters.Repositories.ERepositories;

public class ProductoRepository :
    GRepositories<Producto>,
    IProductoRepository
{
    public ProductoRepository(AppDbContext context) : base(context)
    {
    }
}
