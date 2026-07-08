using Domain.DTO_s.Products;
using Domain.Entities;

namespace Domain.Ports.Repositories.ERepositories;

public interface IProductoRepository :
    IGRepositories<Producto>
{
    Task<Producto?> GetProductByCode(string code, CancellationToken ct);
    Task<IEnumerable<ProductItemDto>> GetbyRangePrice(decimal minPrice, decimal maxPrice, CancellationToken ct);
    Task<ProductItemDto> GetExpensiveProduct(CancellationToken ct);
    Task<ProductItemDto> GetCheapProduct(CancellationToken ct);
    Task<List<MeasureDto>> GetProductByMeasure(string measure, CancellationToken ct);
}
