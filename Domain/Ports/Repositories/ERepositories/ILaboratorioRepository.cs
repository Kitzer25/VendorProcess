using Domain.DTO_s.Laboratory;
using Domain.DTO_s.Products;
using Domain.Entities;

namespace Domain.Ports.Repositories.ERepositories;

public interface ILaboratorioRepository :
    IGRepositories<Laboratorio>
{
    Task<IEnumerable<LaboratoryProductsDto>> GetProductsByLaboratory();
}
