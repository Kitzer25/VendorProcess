using Domain.Entities;
using Domain.Ports.Repositories.ERepositories;
using Infraestructure.Context;

namespace Infraestructure.Adapters.Repositories.ERepositories;

public class RefreshTokenRepository :
    GRepositories<RefreshToken>,
    IRefreshTokenRepository
{
    public RefreshTokenRepository(AppDbContext context) : base(context)
    {
    }
}
