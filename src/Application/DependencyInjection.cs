
using Application.Common.Behaviors;
using Microsoft.Extensions.DependencyInjection;

namespace Application;


// Se define una clase estatic para la configuracion de la inyeccion de dependencias en la aplicacion.
// Esta clase contiene el metodo AddApplication, que se utiliza para configurar los servicios necesarios para la aplicacion.
public static class DependencyInjection
{
     // Metodo de extension para agregar los servicios de la aplicacion al contenedor de dependencias.
    // Este metodo configura MediatR, validadores y una politica de CORS
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
         // Configura MediatR, que es una libreria para manejar peticiones y notificaciones en aplicaciones.
        // El metodo RegisterServicesFromAssemblyContaining permite registrar todos los servicios en el ensamblado que contiene la clase ApplicationAssemblyReferenc
        services.AddMediatR(config => {
            config.RegisterServicesFromAssemblyContaining<ApplicationAssemblyReference>();
        });

        // Se agrega el comportamiento de validacion (ValidationBehavior) al pipeline de MediatR.
        // Esto asegura que las validaciones se ejecuten en las solicitudes antes de que lleguen a su manejador.
        services.AddScoped(
            typeof(IPipelineBehavior<,>),
            typeof(ValidationBehavior<,>)
        );

         // Se agregan los validadores a la coleccion de servicios desde el ensamblado que contiene la clase ApplicationAssemblyReference.
        // Esto permite que todos los validadores definidos en el ensamblado sean inyectados y utilizados en la aplicacion.
        services.AddValidatorsFromAssemblyContaining<ApplicationAssemblyReference>();

        // Agregar política de CORS
        // Se agrega una politica llamada "AllowFrontend" que permite solicitudes desde la URL http://localhost:3000 (comunmente usada en entornos de desarrollo con React, Angular, etc)
        services.AddCors(options =>
        {
            options.AddPolicy("AllowFrontend",
                builder => builder
                    .WithOrigins("http://localhost:3000") // URL del frontend
                    .AllowAnyHeader()// Permite cualquier encabezado
                    .AllowAnyMethod());// Permite cualquier metodo HTTP (GET, POST, PUT, DELETE, etc).
        });

        return services;// Se devuelve la coleccion de servicios configurada para que se pueda continuar con la configuracion de otros servicios.
    }
}
// cambios