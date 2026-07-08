using Domain.DTO_s.DetallePedido;
using Domain.DTO_s.Products;
using Domain.Entities;

namespace Domain.Ports.Repositories.ERepositories;

public interface IDetallePedidoRepository :
    IGRepositories<DetallePedido>
{
    Task<ProductRequiredDto?> GetMostProductRequired(CancellationToken ct);
    Task<ProductLaboratoryDto?> MostRequiredLaboratory(CancellationToken ct);
    
}
