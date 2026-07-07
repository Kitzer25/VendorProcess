using Domain.Entities;
using Domain.Ports.Repositories.ERepositories;
using Insfraestructure.Context;

namespace Insfraestructure.Adapters.Repositories.ERepositories;

public class RefreshTokenRepository :
    GRepositories<RefreshToken>,
    IRefreshTokenRepository
{
    public RefreshTokenRepository(AppDbContext context) : base(context)
    {
    }
}
