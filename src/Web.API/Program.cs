using Application;
using Infrastructure;
using Web.API;
using Web.API.Extensions;
using Web.API.Middlewares;

// Crea un objeto de tipo WebApplicationBuilder usando los argumentos proporcionados al ejecutar la aplicación
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPresentation()// Agrega la capa de presentación (controllers, views, etc.)
                .AddInfrastructure(builder.Configuration) // Agrega la infraestructura, usando la configuración actual
                .AddApplication();// Agrega la capa de aplicación (servicios, lógica de negocio, etc.)

// Construye la aplicación web a partir de la configuración establecida en el builder
var app = builder.Build();

// Aplica una política de CORS llamada "AllowFrontend" para controlar el acceso de dominios externos
app.UseCors("AllowFrontend");


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();// En entorno de desarrollo, habilita Swagger para la documentación de la API
    app.UseSwaggerUI();
    app.ApplyMigrations(); // Aplica migraciones automáticamente para mantener la base de datos actualizada
}

app.UseExceptionHandler("/error");// Configura un controlador de excepciones global que redirige a la ruta "/error" cuando ocurre un error
app.UseHttpsRedirection();// Habilita la redirección automática de HTTP a HTTPS para asegurar las comunicaciones
app.UseAuthorization();// Configura el middleware de autorización
app.UseMiddleware<GloblalExceptionHandlingMiddleware>();// Agrega un middleware(manejar, interceptar y procesar solicitudes HTTP) personalizado para el manejo global de excepciones
app.MapControllers();// Mapea los controladores para que puedan manejar las solicitudes entrantes
app.Run();

