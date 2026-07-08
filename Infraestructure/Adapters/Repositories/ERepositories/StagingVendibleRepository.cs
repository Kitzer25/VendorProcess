using Domain.Entities;
using Domain.Ports.Repositories.ERepositories;
using Infraestructure.Context;

namespace Infraestructure.Adapters.Repositories.ERepositories;

public class StagingVendibleRepository :
    GRepositories<StagingVendible>,
    IStagingVendibleRepository
{
    public StagingVendibleRepository(AppDbContext context) : base(context)
    {
    }
}
