using Web.API.Middlewares;

namespace Web.API;

public static class DependencyInjection
{
    // Método para registrar los servicios necesarios en la capa de presentación
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers();  // Habilita el uso de controladores en la API.
        services.AddEndpointsApiExplorer(); // Agrega soporte para explorar los endpoints disponibles en la API.
        services.AddSwaggerGen();  // Configura Swagger para la documentación automática de la API.
        services.AddTransient<GloblalExceptionHandlingMiddleware>(); // Registra el middleware de manejo global de excepciones.
        return services;
    }
}
