using Domain.Ports.Repositories;
using Domain.Ports.Repositories.ERepositories;
using Infraestructure.Adapters.Repositories;
using Infraestructure.Adapters.Repositories.ERepositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infraestructure.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddInfraestructure(this IServiceCollection services)
    {
        //Main Dependencies
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IGRepositories<>), typeof(GRepositories<>));
        
        //Repositories
        services.AddScoped<IDescuentosProductoRepository, DescuentosProductoRepository>();
        services.AddScoped<IDetallePedidoRepository, DetallePedidoRepository>();
        services.AddScoped<ILaboratorioRepository, LaboratorioRepository>();
        services.AddScoped<IPedidoRepository, PedidoRepository>();
        services.AddScoped<IProductoRepository, ProductoRepository>();
        services.AddScoped<IStagingVendibleRepository, StagingVendibleRepository>();
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();

        return services;
    }
}