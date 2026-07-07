using Domain.Entities;
using Domain.Ports.Repositories.ERepositories;
using Insfraestructure.Context;

namespace Insfraestructure.Adapters.Repositories.ERepositories;

public class LaboratorioRepository :
    GRepositories<Laboratorio>,
    ILaboratorioRepository
{
    public LaboratorioRepository(AppDbContext context) : base(context)
    {
    }
}
