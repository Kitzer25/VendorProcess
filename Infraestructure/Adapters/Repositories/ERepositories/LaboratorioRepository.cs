using Domain.DTO_s.Laboratory;
using Domain.DTO_s.Products;
using Domain.Entities;
using Domain.Ports.Repositories.ERepositories;
using Infraestructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Adapters.Repositories.ERepositories;

public class LaboratorioRepository :
    GRepositories<Laboratorio>,
    ILaboratorioRepository
{
    public LaboratorioRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<LaboratoryProductsDto>> GetProductsByLaboratory()
    {
        return await _dbSet
            .AsNoTracking()
            .Select(l => new LaboratoryProductsDto
            {
                Laboratorie = l.nombre,

                Products = l.productos.Select(p => new ProductItemDto
                {
                    codigo = p.codigo,
                    descripcion = p.descripcion,
                    medida = p.medida,
                    stock = p.stock,
                    precio_venta = p.precio_venta
                })
            })
            .ToListAsync();
    }
}
