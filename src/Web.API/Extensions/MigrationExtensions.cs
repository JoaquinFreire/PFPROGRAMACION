using Microsoft.EntityFrameworkCore;
using Infrastructure.Persistence;

namespace Web.API.Extensions;

public static class MigrationExtensions
{
    // Extensión para aplicar las migraciones pendientes en la base de datos.
    public static void ApplyMigrations(this WebApplication app)
    {
        // Crea un alcance para acceder a los servicios de la aplicación.
        using var scope = app.Services.CreateScope();

        // Obtiene el contexto de la base de datos desde los servicios.
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        // Aplica todas las migraciones pendientes en la base de datos.
        dbContext.Database.Migrate();
    }
}
