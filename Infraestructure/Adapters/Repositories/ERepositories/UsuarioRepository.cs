using Domain.DTO_s.Users;
using Domain.Entities;
using Domain.Ports.Repositories.ERepositories;
using Infraestructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Adapters.Repositories.ERepositories;

public class UsuarioRepository :
    GRepositories<Usuario>,
    IUsuarioRepository
{
    public UsuarioRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<bool> EmailExist(string email, CancellationToken ct)
    {
        return await _dbSet.AnyAsync(p => p.email == email, ct); 
    }

    public async Task<UserInfoDto?> GetByPharamacie(string pharma, CancellationToken ct)
    {
        return await _dbSet.AsNoTracking()
            .Where(w => w.farmacia == pharma)
            .Select(x => new UserInfoDto
            {
                nombre = x.nombre,
                direccion = x.direccion,
                farmacia = x.farmacia,
                telefono = x.telefono
            })
            .FirstOrDefaultAsync(ct);
    }

    public async Task<IEnumerable<UserOrderDto>> GetUserOrdersQunatity(CancellationToken ct)
    {
        return await _dbSet.AsNoTracking()
            .Select(x => new UserOrderDto
            {
                nombre = x.nombre,
                farmacia = x.farmacia,
                orderqunatity = x.pedidoclientes.Count
            }).ToListAsync(ct);
    }
}
