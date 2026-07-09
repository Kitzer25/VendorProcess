using Domain.Entities;
using Domain.Ports.Services.EServices;
using Domain.Ports.Repositories;

namespace Infraestructure.Adapters.Services.EServices;

public class RefreshTokenService :
    IRefreshTokenService
{
    private readonly IUnitOfWork _unitOfWork;

    public RefreshTokenService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    public Task<RefreshToken> GetAllAsync(CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<RefreshToken> GetByIdAsync(int id, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task CreateAsync(RefreshToken entity, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(RefreshToken entity, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(int id, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}
