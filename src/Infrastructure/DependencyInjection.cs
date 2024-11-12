using Application.Data;
using Domain.Customers;
using Domain.Primitives;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    // Método estático que extiende IServiceCollection para agregar la infraestructura de la aplicación
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Llama al método AddPersistence para configurar la persistencia de datos y la agrega al servicio
        services.AddPersistence(configuration);
        // Retorna la colección de servicios configurada
        return services;
    }

    // Método privado estático para agregar la configuración de persistencia a IServiceCollection
    private static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        // Configura el contexto de la base de datos ApplicationDbContext utilizando una conexión de SQL Server
        services.AddDbContext<ApplicationDbContext>(options => 
            options.UseSqlServer(configuration.GetConnectionString("SqlServer")));

        // Registra IApplicationDbContext como un servicio de ámbito (scoped), resolviendo a ApplicationDbContext
        services.AddScoped<IApplicationDbContext>(sp => 
            sp.GetRequiredService<ApplicationDbContext>());

        // Registra IUnitOfWork como un servicio de ámbito (scoped), resolviendo a ApplicationDbContext
        services.AddScoped<IUnitOfWork>(sp => 
            sp.GetRequiredService<ApplicationDbContext>());

        // Registra ICustomerRepository como un servicio de ámbito (scoped), resolviendo a CustomerRepository
        services.AddScoped<ICustomerRepository, CustomerRepository>();

        // Retorna la colección de servicios configurada
        return services;
    }
}
