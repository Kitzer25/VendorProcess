using Domain.DTO_s.DetallePedido;
using Domain.DTO_s.Products;
using Domain.Entities;
using Domain.Ports.Repositories.ERepositories;
using Infraestructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Adapters.Repositories.ERepositories;

public class DetallePedidoRepository :
    GRepositories<DetallePedido>,
    IDetallePedidoRepository
{
    public DetallePedidoRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<ProductRequiredDto?> GetMostProductRequired(CancellationToken ct)
    {
        return await _dbSet.AsNoTracking()
            .GroupBy(x => new
            {
                x.producto_id,
                x.producto.descripcion
            })
            .Select(g => new ProductRequiredDto
            {
                producto_id = g.Key.producto_id,
                producto = g.Key.descripcion,
                cantidad = g.Sum(x => x.cantidad),
                subtotalgenerado =  g.Sum(x => x.subtotal)
            })
            .OrderByDescending(c => c.cantidad)
            .FirstOrDefaultAsync(ct);
    }

    public async Task<ProductLaboratoryDto?> MostRequiredLaboratory(CancellationToken ct)
    {
        return await  _dbSet.AsNoTracking()
            .GroupBy(x => new
            {
                x.producto.laboratorio.nombre,
                x.producto.descripcion
                
            })
            .Select(l => new ProductLaboratoryDto
            {
                laboratorie = l.Key.nombre,
                product = l.Key.descripcion,
                qunatity = l.Count()
            })
            .OrderByDescending(x => x.qunatity)
            .FirstOrDefaultAsync(ct);
    }
}
