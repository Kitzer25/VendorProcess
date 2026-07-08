using Domain.DTO_s.Products;
using Domain.Entities;
using Domain.Ports.Repositories.ERepositories;
using Infraestructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Adapters.Repositories.ERepositories;

public class ProductoRepository :
    GRepositories<Producto>,
    IProductoRepository
{
    public ProductoRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<Producto?> GetProductByCode(
        string code,
        CancellationToken ct)
    {
        return await _dbSet.AsNoTracking()
            .Where(p => p.codigo == code)
            .FirstOrDefaultAsync(ct);
    }

    public async Task<IEnumerable<ProductItemDto>> GetbyRangePrice(
        decimal minPrice,
        decimal maxPrice,
        CancellationToken ct)
    {
        return await _dbSet.AsNoTracking()
            .Where(p => p.precio_venta >= minPrice && p.precio_venta <= maxPrice)
            .Select(x => new ProductItemDto
            {
                codigo = x.codigo,
                descripcion = x.descripcion,
                precio_venta = x.precio_venta,
                medida = x.medida,
                stock = x.stock,
            })
            .ToListAsync(ct);
    }

    public async Task<ProductItemDto> GetExpensiveProduct(CancellationToken ct)
    {
        return await _dbSet.AsNoTracking()
            .Select(x => new ProductItemDto
            {
                codigo = x.codigo,
                descripcion = x.descripcion,
                precio_venta = x.precio_venta,
                medida = x.medida,
                stock = x.stock
            })
            .OrderByDescending(o => o.precio_venta)
            .FirstAsync(ct);
    }

    public async Task<ProductItemDto> GetCheapProduct(CancellationToken ct)
    {
        return await _dbSet.AsNoTracking()
            .Select(x => new ProductItemDto
            {
                codigo = x.codigo,
                descripcion = x.descripcion,
                precio_venta = x.precio_venta,
                medida = x.medida,
                stock = x.stock
            })
            .OrderBy(o => o.precio_venta)
            .FirstAsync(ct);
    }

    public async Task<List<MeasureDto>> GetProductByMeasure(string measure,
        CancellationToken ct)
    {
        return await _dbSet.AsNoTracking()
            .Where(p => p.medida == measure)
            .Select(x => new MeasureDto
            {
                Medida = x.medida,
                Product = new ProductDto
                {
                    codigo = x.codigo,
                    descripcion = x.descripcion,
                    stock = x.stock,
                    precio_venta = x.precio_venta
                }
            })
            .ToListAsync(ct);
    }
}
