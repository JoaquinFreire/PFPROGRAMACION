using System.Net;
using System.Text.Json;

namespace Web.API.Middlewares;

public class GloblalExceptionHandlingMiddleware : IMiddleware
{
    private readonly ILogger<GloblalExceptionHandlingMiddleware> _logger;

    // Constructor que recibe un logger para registrar errores.
    public GloblalExceptionHandlingMiddleware(ILogger<GloblalExceptionHandlingMiddleware> logger) => _logger = logger;

    // Método principal que captura excepciones en la ejecución de la solicitud.
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            // Continúa la ejecución de la solicitud.
            await next(context);
        }
        catch (Exception e)
        {
            // Si ocurre una excepción, se registra el error.
            _logger.LogError(e, e.Message);

            // Se establece el código de estado HTTP 500 (Error interno del servidor).
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            // Se crea un objeto ProblemDetails para proporcionar información sobre el error.
            ProblemDetails problem = new()
            {
                Status = (int)HttpStatusCode.InternalServerError,
                Type = "Server Error",
                Title = "Server Error",
                Detail = "An internal server has ocurred."
            };

            // Se convierte el objeto ProblemDetails a formato JSON.
            string json = JsonSerializer.Serialize(problem);

            // Se define el tipo de contenido como JSON.
            context.Response.ContentType = "application/json";

            // Se escribe la respuesta con el detalle del error.
            await context.Response.WriteAsync(json);
        }
    }
}
