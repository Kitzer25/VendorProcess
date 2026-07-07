using Domain.Entities;
using Domain.Ports.Repositories.ERepositories;
using Insfraestructure.Context;

namespace Insfraestructure.Adapters.Repositories.ERepositories;

public class StagingVendibleRepository :
    GRepositories<StagingVendible>,
    IStagingVendibleRepository
{
    public StagingVendibleRepository(AppDbContext context) : base(context)
    {
    }
}
