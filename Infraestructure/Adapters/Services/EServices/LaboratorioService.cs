using Domain.Entities;
using Domain.Ports.Services.EServices;
using Domain.Ports.Repositories;

namespace Infraestructure.Adapters.Services.EServices;

public class LaboratorioService :
    ILaboratorioService
{
    private readonly IUnitOfWork _unitOfWork;

    public LaboratorioService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    public Task<Laboratorio> GetAllAsync(CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<Laboratorio> GetByIdAsync(int id, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task CreateAsync(Laboratorio entity, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(Laboratorio entity, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(int id, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}
