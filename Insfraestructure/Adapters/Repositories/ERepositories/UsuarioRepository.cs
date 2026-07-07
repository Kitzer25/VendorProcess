using Domain.Entities;
using Domain.Ports.Repositories.ERepositories;
using Insfraestructure.Context;

namespace Insfraestructure.Adapters.Repositories.ERepositories;

public class UsuarioRepository :
    GRepositories<Usuario>,
    IUsuarioRepository
{
    public UsuarioRepository(AppDbContext context) : base(context)
    {
    }
}
